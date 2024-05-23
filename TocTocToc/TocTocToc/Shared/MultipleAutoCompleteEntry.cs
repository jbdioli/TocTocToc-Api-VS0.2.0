using System;
using System.Collections.Generic;
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

    private bool _isEndingChar = false;
    private ErrorModel _error = new();
    private bool _isEditedText = false;
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


    public async Task TextChanged(TextChangedEventArgs e, int cursorPosition)
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


    private Task TextChangedHandler(TextChangedEventArgs e)
    {
        var previousText = e.OldTextValue;
        var newTextValue = e.NewTextValue;

        if (string.IsNullOrWhiteSpace(previousText) && string.IsNullOrEmpty(newTextValue))
        {
            _autoCompleteEntry.EntryItems.Clear();
            _isEndingChar = false;
            return Task.CompletedTask;
        }


        // Text recognition
        (_isEndingChar, _error) = _textHandler.TextRecognition(_text.Text);
        if (_isEndingChar)
        {
            HideSuggestions();
            return Task.CompletedTask;
        }

        var isTreatedError = IsHandelError(_error);
        if (isTreatedError) return Task.CompletedTask;

        if (!string.IsNullOrEmpty(previousText))
        {
            var compareText = string.Compare(previousText.Trim(), newTextValue.Trim(), StringComparison.Ordinal);
            if (compareText == 0)
            {
                _isEndingChar = false;
                return Task.CompletedTask;
            }
        }

        (_isEditedText, _error) = _textHandler.EditTextHandler(newTextValue, previousText);
        if (!_isEditedText)
        {
            var isDeleted = _textHandler.DeleteWordsFromText();
            if (isDeleted)
            {
                _autoCompleteEntry.EntryItems.Clear();
                CopyModel.WordModelsToItems(_text.Words, _autoCompleteEntry.EntryItems);
            }
        }

        IsHandelError(_error);
        return Task.CompletedTask;


        //_textHandler.RefreshTextHandler();
    }


    // to see with HandleTextCompleted(TextModel text)
    private async Task HandleTextCompleted()
    {
        _autoCompleteEntry.EventOrder.TextCompletedDisable = true;
        _autoCompleteEntry.EventOrder.TextChangedDisable = true;

        if (_text.Words is { Count: > 0 } && _text.IsInvalid && _isEditedText)
        {
            await _textHandler.CheckWordsValidityTask();
        }
        else
        {
            await _textHandler.AddWordsFromTextTask();
        }

        if (_text.IsInvalid)
        {
            _textHandler.DeleteInvalidWords();
            _text.Text = string.Empty;
            _textHandler.AddWordsToText();
            DeleteInvalidItems();
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

    private void DeleteInvalidItems()
    {
        _autoCompleteEntry.EntryItems.Clear();

        if (_text.Words == null) return;

        foreach (var word in _text.Words.TakeWhile(el => !el.IsInvalid) )
        {
            var item = new ItemModel(){Id = 0, IdParents = 0, Item = word.Word};
            AddToEntryItems(item);
        }

        AddItemsToText();

    }

    private void AddItemsToText()
    {
        if (_text.SeparatorList.Count == 0) return;
        var separator = _text.SeparatorList[0];

        var items = _autoCompleteEntry.EntryItems.Select(item => item.Item).ToList();
        if (items.Count == 0)
        {
            _autoCompleteEntry.Text = string.Empty;
        }
        else
        {
            _autoCompleteEntry.Text = string.Join(separator + " ", items) + separator + " ";
        }
    }



    private bool IsHandelError(ErrorModel error)
    {
        var isTreatedError = true;

        if (error != null && (error.IsWrongChar || error.IsWrongWord))
        {
            _autoCompleteEntry.EventOrder.TextChangedDisable = true;
            if (error.IsWrongChar)
            {
                _textHandler.DeleteWrongChar();
                _autoCompleteEntry.Text = _text.Text;
                NOTIFICATION_HANDLER.SendNotification(ENotificationType.IncorrectChar, null);
            }

            if (error.IsWrongWord)
            {
                _autoCompleteEntry.EntryItems.Clear();
                _textHandler.DeleteWrongWords();
                foreach (var word in _text.Words)
                {
                    if (word.IsInvalid) continue;
                    var item = new ItemModel();
                    CopyModel.WordModelToItem(word, item);
                    AddToEntryItems(item);
                }
                NOTIFICATION_HANDLER.SendNotification(ENotificationType.IncorrectValidWord, null);
            }
        }
        else
        {
            isTreatedError = false;
        }

        _autoCompleteEntry.Error = error;
        return isTreatedError;
    }


    private List<ItemModel> DeleteWordsHandler()
    {
        _textHandler.DeleteWordsFromText();
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