using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class MultipleAutoCompleteEntry: IAutoCompeteEntry
{

    private readonly AutoCompleteEntryModel _autoCompleteEntry;
    private readonly TextModel _text = new();

    private readonly TextHandler _textHandler;

    private static readonly NotificationChannelHandler NOTIFICATION_HANDLER = new(new DisplayNotification());

    public bool IsEndingChar = false;
    public bool IsCompletedEntry { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public MultipleAutoCompleteEntry(AutoCompleteEntryModel autoCompleteEntry)
    {
        _autoCompleteEntry = autoCompleteEntry;
        _textHandler = new TextHandler(_text);
        InitData();
    }


    private void InitData()
    {
        var itemsToDisplay = string.Empty;

        if (IsEditing)
        {
            _autoCompleteEntry.EventOrder.TextChangedDisable = true;
            _autoCompleteEntry.EventOrder.UnfocusedDisable = true;
        }

        if (!string.IsNullOrWhiteSpace(_autoCompleteEntry.Text))
            itemsToDisplay = _autoCompleteEntry.Text;
        
        if (_autoCompleteEntry.EntryItems is { Count: > 0 })
        {
            foreach (var itemDto in _autoCompleteEntry.EntryItems)
            {
                _text.Words.Add(new WordModel(){Word = itemDto.Item});
            }
            _textHandler.AddWordsToText();
            itemsToDisplay = _text.Text;
        }

        _autoCompleteEntry.Text = itemsToDisplay;
        _text.SeparatorList = [";", ","];

    }


    public async Task TextChanged(TextChangedEventArgs e)
    {
        if (_autoCompleteEntry.EventOrder.TextChangedDisable)
        {
            _autoCompleteEntry.EventOrder.TextChangedDisable = false;
            return;
        }

        ShowSuggestions();

        try
        {
            _text.Text = e.NewTextValue;

            await TextChangedHandler(e);

            if (string.IsNullOrWhiteSpace(_text.Text))
            {
                _textHandler.Clear();
                HideSuggestions();
                return;
            }

            // Text recognition
            (IsEndingChar, var isWrongChar) = _textHandler.TextRecognition(_text.Text);
            if (IsEndingChar)
            {
                await HandleTextCompleted();
                return;
            }

            if (isWrongChar)
            {
                _textHandler.DeleteLastChar();
                NOTIFICATION_HANDLER.SendNotification(ENotificationType.IncorrectValidWord, null);
            }

            // Handles suggestions to be display in the suggestion view
            HandelSuggestionsView();
        }
        catch (Exception)
        {
            _autoCompleteEntry.ItemSuggestions.Clear();
            _autoCompleteEntry.IsSuggestionView = false;
            HideSuggestions();
        }
    }
    

    public Task ItemTapped(ItemModel itemModel)
    {
        var word = new WordModel { Word = itemModel?.Item, IsInvalid = false, Dictionary = new DictionaryDtoModel()};

        _text.Words.Add(word);

        _textHandler.AddWordsToText();

        _autoCompleteEntry.EventOrder.TextChangedDisable = true;
        _autoCompleteEntry.EventOrder.TextCompletedDisable = true;
        _autoCompleteEntry.EventOrder.UnfocusedDisable = true;

        _autoCompleteEntry.Text = _text.Text;

        AddToEntryItems(itemModel);

        HideSuggestions();
        return Task.CompletedTask;
    }


     public async Task TextCompleted()
    {
        if (_autoCompleteEntry.EventOrder.TextCompletedDisable)
        {
            _autoCompleteEntry.EventOrder.TextCompletedDisable = false;
            return;
        }


        if (string.IsNullOrWhiteSpace(_text.Text))
        {
            HideSuggestions();
            return;
        }

        if (!_text.IsInvalid)
        {
            HideSuggestions();
            return;
        }

        await HandleTextCompleted();
        HideSuggestions();
    }

    public async Task Unfocused()
    {
        if (_autoCompleteEntry.EventOrder.UnfocusedDisable)
        {
            _autoCompleteEntry.EventOrder.UnfocusedDisable = false;
            return;
        }

        if (string.IsNullOrWhiteSpace(_text.Text))
        {
            HideSuggestions();
            return;
        }

        await HandleTextCompleted();
        ResetEntryEvents();
        HideSuggestions();
    }


    public void ShowSuggestions()
    {
        new DisplayAutoCompleteHandler(_autoCompleteEntry).ShowSuggestions();
    }


    public void HideSuggestions()
    {
        new DisplayAutoCompleteHandler(_autoCompleteEntry).HideSuggestions();
        _autoCompleteEntry.ItemSuggestions.Clear();
    }

    // to see with HandleTextCompleted(TextModel text)
    private async Task HandleTextCompleted()
    {
        _autoCompleteEntry.EventOrder.TextCompletedDisable = true;
        _autoCompleteEntry.EventOrder.TextChangedDisable = true;

        await _textHandler.AddWordsFromTextTask();

        if (_text.IsInvalid)
        {
            DeleteInvalidWords();
            HideSuggestions();
            return;
        }

        _autoCompleteEntry.Text = _text.Text;

        foreach (var wordDto in _text.Words)
        {
            var itemModel = new ItemModel() { Id = 0, IdParents = 0, Item = wordDto.Word };
            AddToEntryItems(itemModel);
        }

        HideSuggestions();
    }


    private string GetCurrentWord()
    {
        return _textHandler.GetCurrentWord(_text.Text);
    }

    private void DeleteInvalidWords()
    {
        _autoCompleteEntry.EntryItems.Clear();

        foreach (var word in _text.Words.TakeWhile(v => v.IsInvalid) )
        {
            var item = new ItemModel(){Id = 0, IdParents = 0, Item = word.Word};
            AddToEntryItems(item);
        }

        _textHandler.AddWordsToText();

    }


    private async Task TextChangedHandler(TextChangedEventArgs e)
    {
        var previousText = e.OldTextValue;
        var newTextValue = e.NewTextValue;

        if (string.IsNullOrWhiteSpace(previousText) && string.IsNullOrEmpty(newTextValue))
        {
            _autoCompleteEntry.EntryItems.Clear();
            return;
        }
        if (string.IsNullOrEmpty(previousText)) return;

        var compareText = string.Compare(previousText.Trim(), newTextValue.Trim(), StringComparison.Ordinal);
        if (compareText == 0) return;

        if (_autoCompleteEntry.EntryItems.Count == 0) return;



        var items = new List<ItemModel>();

        var words= await _textHandler.EditTextHandler(newTextValue, previousText);
        if (words.Count == 0)
        {
            items = DeleteWordsHandler(previousText);
        }
        else
        {
            foreach (var wordModel in words)
            {
                var item = new ItemModel()
                {
                    Item = wordModel.Word,
                    Id = 0,
                    IdParents = 0
                };
                item = CheckItemInDataBase(item);
                items.Add(item);
            }
        }

        _autoCompleteEntry.EntryItems.Clear();
        _autoCompleteEntry.EntryItems = new ObservableCollection<ItemModel>(items);

    }


    private List<ItemModel> DeleteWordsHandler(string previousText)
    {
        _textHandler.DeleteWordsFromText(previousText);
        var items = _autoCompleteEntry.EntryItems.Where((item) => _text.Words.Exists(word => word.Word.Equals(item.Item))).ToList();

        return items;

    }


    private void HandelSuggestionsView()
    {
        var word = GetCurrentWord();
        var values = _autoCompleteEntry.ItemProposals.Where(i => i.Item.ToLower().Contains(word.ToLower())).ToList();
        _autoCompleteEntry.ItemSuggestions.Clear();
        if (!values.Any())
        {
            HideSuggestions();
        }
        else
        {
            foreach (var item in values)
            {
                AddItemToItemSuggestions(item);
            }
        }
    }


    private void AddItemToItemSuggestions(ItemModel item)
    {
        var isExisting = _autoCompleteEntry.ItemSuggestions
            .Select(el => el.Item.ToLower().Equals(item.Item.ToLower())).LastOrDefault(el => el.Equals(true));
        if (!isExisting)
            _autoCompleteEntry.ItemSuggestions.Add(item);
    }


    private void AddToEntryItems(ItemModel itemSubmitted)
    {
        _autoCompleteEntry.EntryItems ??= [];
        
        var isExisting = _autoCompleteEntry.EntryItems
            .Select(el => el.Item.ToLower().Equals(itemSubmitted.Item.ToLower())).LastOrDefault(el => el.Equals(true));
        if (!isExisting)
        {
            var itemFound = CheckItemInDataBase(itemSubmitted);
            _autoCompleteEntry.EntryItems.Add(new ItemModel() { Id = itemFound.Id, IdParents = itemFound.IdParents, Item = itemFound.Item });
        }
    }


    private ItemModel CheckItemInDataBase( ItemModel itemSubmitted)
    {
        var itemFound = _autoCompleteEntry.ItemProposals.FirstOrDefault(el => el.Item.ToLower().Equals(itemSubmitted.Item.ToLower()));
        return itemFound ?? itemSubmitted;
    }



    private void ResetEntryEvents()
    {
        //unfocused event finished so all event authorized
        _autoCompleteEntry.EventOrder.TextChangedDisable = false;
        _autoCompleteEntry.EventOrder.ItemTappedDisable = false;
        _autoCompleteEntry.EventOrder.TextCompletedDisable = false;
    }


}