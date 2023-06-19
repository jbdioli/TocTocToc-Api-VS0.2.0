using System;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class TokenAutoCompleteEntry: IAutoCompeteEntry
{
    private readonly WordHandler _wordHandler;

    private readonly AutoCompleteEntryDtoModel _autoCompleteEntryDto;
    private readonly WordDtoModel _wordDto = new();
    private bool _isEditing = false;

    public TokenAutoCompleteEntry(AutoCompleteEntryDtoModel autoCompleteEntryDto)
    {
        _autoCompleteEntryDto = autoCompleteEntryDto;
        _wordHandler = new WordHandler(_wordDto);
        InitData();
    }


    private void InitData()
    {
        if (_autoCompleteEntryDto == null || _autoCompleteEntryDto != null && string.IsNullOrWhiteSpace(_autoCompleteEntryDto.Text)) return;
        var index = new DisplayAutoCompleteHandler(_autoCompleteEntryDto).FindFocus();
        if (index < (_autoCompleteEntryDto.XNameEntries.Count - 1))
            _isEditing = true;
    }


    public void ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (_isEditing)
        {
            _isEditing = false;
            return;
        }

        var itemView = e.Item as ItemViewModel;

        _wordDto.WordRequested = itemView?.Item;
        _wordDto.Invalid = false;

        _autoCompleteEntryDto.EventOrder.TextChangedDisable = true;
        _autoCompleteEntryDto.EventOrder.TextCompletedDisable = true;
        _autoCompleteEntryDto.EventOrder.UnfocusedDisable = true;

        _autoCompleteEntryDto.Text = itemView?.Item;
        _autoCompleteEntryDto.XNameEntries[_autoCompleteEntryDto.IndexOfSelectedEntry].Text = string.Empty;

        AddToken(itemView);

        HideSuggestions();

        ((ListView)sender).SelectedItem = null;
    }

    public Task TextChanged(TextChangedEventArgs e)
    {
        if (_isEditing)
        {
            _isEditing = false;
            return Task.CompletedTask;
        }

        if (_autoCompleteEntryDto.EventOrder.TextChangedDisable)
        {
            _autoCompleteEntryDto.EventOrder.TextChangedDisable = false;
            return Task.CompletedTask;
        }

        ShowSuggestions();

        _autoCompleteEntryDto.XNameSuggestionView.BeginRefresh();

        try
        {
            _wordDto.WordRequested = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(_wordDto.WordRequested))
            {
                HideSuggestions();
                _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
                return Task.CompletedTask;
            }

            _wordHandler.CleaningWordDefinition(); // Reset definition in case the word as been modified

            var values = _autoCompleteEntryDto.Suggestions.Where(i => i.Item.ToLower().Contains(_wordDto.WordRequested.ToLower())).ToList();
            if (!values.Any())
            {
                HideSuggestions();
            }
            else
                _autoCompleteEntryDto.XNameSuggestionView.ItemsSource = values;

        }
        catch (Exception)
        {
            HideSuggestions();
        }

        _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
        return Task.CompletedTask;
    }

    public async Task TextCompleted(object sender)
    {
        if (_isEditing)
        {
            _isEditing = false;
            return;
        }

        if (_autoCompleteEntryDto.EventOrder.TextCompletedDisable)
        {
            _autoCompleteEntryDto.EventOrder.TextCompletedDisable = false;
            return;
        }
        else
        {
            _autoCompleteEntryDto.EventOrder.UnfocusedDisable = true;
        }

        if (string.IsNullOrWhiteSpace(_wordDto.WordRequested))
            return;

        await HandleWordCompleted();
        HideSuggestions();

    }

    public async Task Unfocused()
    {
        if (_isEditing)
        {
            _isEditing = false;
            return;
        }

        await Task.Delay(200);

        if (_autoCompleteEntryDto.EventOrder.UnfocusedDisable)
        {
            _autoCompleteEntryDto.EventOrder.UnfocusedDisable = false;
            return;
        }

        if (string.IsNullOrWhiteSpace(_wordDto.WordRequested))
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
        new DisplayAutoCompleteHandler(_autoCompleteEntryDto).ShowSuggestions();
    }

    public void HideSuggestions()
    {
        new DisplayAutoCompleteHandler(_autoCompleteEntryDto).HideSuggestions();

    }


    private async Task HandleWordCompleted()
    {
        var indexSelectedEntry = _autoCompleteEntryDto.IndexOfSelectedEntry;

        _wordHandler.CleaningWord();
        var isValidWord = await IsWordValid();
        if (!isValidWord)
        {
            DeleteWordFromEntry();
            return;
        }
        AddToElementToAdd(_wordDto.WordRequested);
        AddToken(new ItemViewModel(){Id = 0, IdParents = 0, Item = _wordDto.WordRequested});
        _autoCompleteEntryDto.XNameEntries[indexSelectedEntry].Text = string.Empty;

    }

    private async Task<bool> IsWordValid()
    {
        await _wordHandler.CtrlWordValidity();
        return !_wordDto.Invalid;
    }

    private void DeleteWordFromEntry()
    {
        var activeEntry = _autoCompleteEntryDto.IndexOfSelectedEntry;
        _autoCompleteEntryDto.XNameEntries[activeEntry].Text = string.Empty;
    }

    

    private void AddToElementToAdd(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return;

        var isElement = _autoCompleteEntryDto.NewElementsToAdd
            .Select(el => el.Item.ToLower().Equals(word.ToLower())).LastOrDefault(el => el.Equals(true));
        if (!isElement)
            _autoCompleteEntryDto.NewElementsToAdd.Add(new ItemDtoModel() { Item = word });

    }


    private void AddToken(ItemViewModel itemDto)
    {
        if (_autoCompleteEntryDto.ElementsToDisplay == null) return;

        var isExisting = _autoCompleteEntryDto.ElementsToDisplay
            .Select(el => el.Item.ToLower().Equals(itemDto.Item.ToLower())).LastOrDefault(el => el.Equals(true));
        if (!isExisting)
            _autoCompleteEntryDto.ElementsToDisplay.Add(new ItemViewModel() { Id = itemDto.Id, IdParents = itemDto.IdParents, Item = itemDto.Item});


        //foreach (var token in _autoCompleteEntryDto.NewElementsToAdd)
        //{
        //    var isToken = _autoCompleteEntryDto.ElementsToDisplay
        //        .Select(el => el.Item.ToLower().Equals(token.Word.ToLower())).LastOrDefault(el => el.Equals(true));
        //    if (!isToken)
        //        _autoCompleteEntryDto.ElementsToDisplay.Add(new ItemDtoModel() { Id = 0, IdParents = 0, Item = token.Word });

        //}
    }
}