using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class TokenAutoCompleteEntry: IAutoCompeteEntry
{
    private readonly WordHandler _wordHandler;

    private readonly AutoCompleteEntryModel _autoCompleteEntry;
    private readonly WordModel _word = new();
    public bool IsCompletedEntry { get; set; }
    public bool IsEditing { get; set; } = false;



    public TokenAutoCompleteEntry(AutoCompleteEntryModel autoCompleteEntry)
    {
        _autoCompleteEntry = autoCompleteEntry;
        _wordHandler = new WordHandler(_word);
    }


    public Task TextChanged(TextChangedEventArgs e, int cursorPosition)
    {
        if (IsEditing)
        {
            IsEditing = false;
            return Task.CompletedTask;
        }

        if (_autoCompleteEntry.EventOrder.TextChangedDisable)
        {
            _autoCompleteEntry.EventOrder.TextChangedDisable = false;
            return Task.CompletedTask;
        }

        ShowSuggestions();


        try
        {
            _word.Word = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(_word.Word))
            {
                HideSuggestions();
                return Task.CompletedTask;
            }

            var values = _autoCompleteEntry.ItemProposals.Where(i => i.Item.ToLower().Contains(_word.Word.ToLower())).ToList();
            if (!values.Any())
            {
                HideSuggestions();
            }
            else
            {
                foreach (var item in values)
                {
                    AddToItemFoundCollection(item);
                }
            }

        }
        catch (Exception)
        {
            HideSuggestions();
        }

        return Task.CompletedTask;
    }


    public Task ItemTapped(ItemModel itemModel)
    {
        if (IsEditing)
        {
            IsEditing = false;
            return Task.CompletedTask;
        }

        _word.Word = itemModel?.Item;
        _word.IsInvalid = false;

        _autoCompleteEntry.EventOrder.TextChangedDisable = true;
        _autoCompleteEntry.EventOrder.TextCompletedDisable = true;
        _autoCompleteEntry.EventOrder.UnfocusedDisable = true;

        _autoCompleteEntry.Text = itemModel?.Item;

        if (itemModel != null)
        {
            _autoCompleteEntry.EntryItems.Add(itemModel);
        }

        HideSuggestions();
        return Task.CompletedTask;
    }



    public async Task TextCompleted()
    {
        if (IsEditing)
        {
            IsEditing = false;
            return;
        }

        if (_autoCompleteEntry.EventOrder.TextCompletedDisable)
        {
            _autoCompleteEntry.EventOrder.TextCompletedDisable = false;
            return;
        }
        else
        {
            _autoCompleteEntry.EventOrder.UnfocusedDisable = true;
        }

        if (string.IsNullOrWhiteSpace(_word.Word))
            return;

        await HandleWordCompleted();
        HideSuggestions();

    }


    public async Task Unfocused()
    {
        if (IsEditing)
        {
            IsEditing = false;
            return;
        }

        await Task.Delay(200);

        if (_autoCompleteEntry.EventOrder.UnfocusedDisable)
        {
            _autoCompleteEntry.EventOrder.UnfocusedDisable = false;
            return;
        }

        if (string.IsNullOrWhiteSpace(_word.Word))
            return;

        await HandleWordCompleted();
        HideSuggestions();
    }

    public bool IsNextOrEndingCharacter(string text)
    {
        return false;
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


    private async Task HandleWordCompleted()
    {
        _wordHandler.FormatWord();
        var isValidWord = await IsWordValid();
        if (!isValidWord)
        {
            DeleteWordFromEntry();
            HideSuggestions();
            return;
        }

        var itemModel = new ItemModel() { Id = 0, IdParents = 0, Item = _word.Word };
        var itemDto = new ItemDtoModel() { Id = 0, IdParents = 0, Item = _word.Word };

        AddToElementToDisplay(itemModel);
        AddToNewElement(itemDto);

        HideSuggestions();
    }

    private async Task<bool> IsWordValid()
    {
        await _wordHandler.CheckWordValidityTask();
        return !_word.IsInvalid;
    }

    private void DeleteWordFromEntry()
    {
        //var activeEntry = _autoCompleteEntry.IndexOfSelectedEntry;
        //_autoCompleteEntry.XNameEntries[activeEntry].Text = string.Empty;
    }


    private void AddToItemFoundCollection(ItemModel item)
    {
        var isExisting = _autoCompleteEntry.ItemSuggestions
            .Select(el => el.Item.ToLower().Equals(item.Item.ToLower())).LastOrDefault(el => el.Equals(true));
        if (!isExisting)
            _autoCompleteEntry.ItemSuggestions.Add(item);
    }


    private void AddToNewElement(ItemDtoModel itemDto)
    {
        var newElement = CheckItemInDataBase(itemDto);

        if (newElement == null) return;

        var isExisting = _autoCompleteEntry.EntryItems
            .Select(el => el.Item.ToLower().Equals(itemDto.Item.ToLower())).LastOrDefault(el => el.Equals(true));

        if (!isExisting)
            _autoCompleteEntry.EntryItems.Add(new ItemModel() { Item = itemDto.Item });

    }


    private void AddToElementToDisplay(ItemModel itemModel)
    {
        if (_autoCompleteEntry.EntryItems == null) return;

        _autoCompleteEntry.EntryItems ??= [];

        var isExisting = _autoCompleteEntry.EntryItems
            .Select(el => el.Item.ToLower().Equals(itemModel.Item.ToLower())).LastOrDefault(el => el.Equals(true));
        if (!isExisting)
            _autoCompleteEntry.EntryItems.Add(new ItemModel() { Id = itemModel.Id, IdParents = itemModel.IdParents, Item = itemModel.Item });
    }

    private ItemDtoModel CheckItemInDataBase(ItemDtoModel itemDto)
    {
        var isExisting = _autoCompleteEntry.ItemProposals
            .Select(el => el.Item.ToLower().Equals(itemDto.Item.ToLower())).LastOrDefault(el => el.Equals(true));
        return !isExisting ? itemDto : null;
    }

}