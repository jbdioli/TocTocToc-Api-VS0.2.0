using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class MultipleAutoCompleteEntry: IAutoCompeteEntry
{

    private readonly AutoCompleteEntryDtoModel _autoCompleteEntryDto;
    private readonly TextDtoModel _textDto = new();

    private readonly TextHandler _textHandler;

    //private static readonly NotificationChannelHandler NotificationHandler = new(new DisplayNotification());

    private bool _isEditing = false;


    public MultipleAutoCompleteEntry(AutoCompleteEntryDtoModel autoCompleteEntryDto)
    {
        _autoCompleteEntryDto = autoCompleteEntryDto;
        _textHandler = new TextHandler(_textDto);
        DefaultDtoInit();
        InitData();
    }


    private void InitData()
    {
        var itemsToDisplay = string.Empty;

        if (_autoCompleteEntryDto?.XNameEntries == null) return;
        var index = new DisplayAutoCompleteHandler(_autoCompleteEntryDto).FindFocus();

        if (!string.IsNullOrWhiteSpace(_autoCompleteEntryDto.Text))
            itemsToDisplay = _autoCompleteEntryDto.Text;
        
        if (_autoCompleteEntryDto.ElementsToDisplay is { Count: > 0 })
        {
            foreach (var itemDto in _autoCompleteEntryDto.NewElementsToAdd)
            {
                _textDto.Words.Add(new WordDtoModel(){WordRequested = itemDto.Item});
            }
            _textHandler.AddWordsToTextRequested();
            itemsToDisplay = _textDto.TextRequested;
        }

        _autoCompleteEntryDto.XNameEntries[index].Text = itemsToDisplay;

        if (index < (_autoCompleteEntryDto.XNameEntries.Count - 1))
            _isEditing = true;
    }

    private void DefaultDtoInit()
    {
        _textDto.SeparatorList = new List<string>() { ";", "," };
    }

    public void ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (_isEditing)
        {
            _isEditing = false;
            return;
        }

        var itemView = e.Item as ItemViewModel;

        var words = new List<WordDtoModel> { new WordDtoModel(){WordRequested = itemView?.Item, Invalid = false} };
        _textHandler.AddWordsToWordsDto(words);

        _autoCompleteEntryDto.EventOrder.TextChangedDisable = true;
        _autoCompleteEntryDto.EventOrder.TextCompletedDisable = true;
        _autoCompleteEntryDto.EventOrder.UnfocusedDisable = true;

        _textHandler.AddWordsToTextRequested();

        _autoCompleteEntryDto.Text = _textDto.TextRequested;
        var activeEntry = _autoCompleteEntryDto.IndexOfSelectedEntry;
        _autoCompleteEntryDto.XNameEntries[activeEntry].Text = _autoCompleteEntryDto.Text;

        AddToElementToDisplay(itemView);

        HideSuggestions();

        ((ListView)sender).SelectedItem = null;
    }



    public async Task TextChanged(TextChangedEventArgs e)
    {
        if (_isEditing)
        {
            _isEditing = false;
            return;
        }

        if (_autoCompleteEntryDto.EventOrder.TextChangedDisable)
        {
            _autoCompleteEntryDto.EventOrder.TextChangedDisable = false;
            return;
        }

        ShowSuggestions();

        _autoCompleteEntryDto.XNameSuggestionView.BeginRefresh();

        try
        {
            _textDto.TextRequested = e.NewTextValue;


            var previousText = e.OldTextValue;

            _textHandler.DeleteWordsFromTextRequested(previousText);


            if (string.IsNullOrWhiteSpace(_textDto.TextRequested))
            {
                _textHandler.CleaningTextDefinition();
                HideSuggestions();
                _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
                return;
            }

            var endingCharacter = IsNextOrEndingCharacter(_textDto.TextRequested);
            if (endingCharacter)
            {
                await HandleTextCompleted();
                return;
            }

            var word = GetLastWord();
            var values = _autoCompleteEntryDto.Suggestions.Where(i => i.Item.ToLower().Contains(word.ToLower())).ToList();
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
            

        _textDto.TextRequested = ((Entry)sender).Text;

        if (string.IsNullOrWhiteSpace(_textDto.TextRequested))
        {
            HideSuggestions();
            _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
            return;
        }

        await HandleTextCompleted();

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
        else
        {
            _autoCompleteEntryDto.EventOrder.TextChangedDisable = true;
            _autoCompleteEntryDto.EventOrder.ItemTappedDisable = true;
            _autoCompleteEntryDto.EventOrder.TextCompletedDisable = true;
        }

        if (string.IsNullOrWhiteSpace(_textDto.TextRequested))
        {
            HideSuggestions();
            _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
            return;
        }

        await HandleTextCompleted();

        // unfocused event finished so all event authorized
        _autoCompleteEntryDto.EventOrder.TextChangedDisable = false;
        _autoCompleteEntryDto.EventOrder.ItemTappedDisable = false;
        _autoCompleteEntryDto.EventOrder.TextCompletedDisable = false;

    }

    public bool IsNextOrEndingCharacter(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException("", "[ERROR] - In the method CheckNextOrEndingCharacter, Text can't be empty or null");
        }

        var lastCharacter = text.Substring(text.Length - 1);
        return !lastCharacter.All(c => (char.IsLetter(c) || c == (int)EDecimalCharacter.Space || c == (int)EDecimalCharacter.Hyphen || c == (int)EDecimalCharacter.Apostrophe));
    }

    public void ShowSuggestions()
    {
        new DisplayAutoCompleteHandler(_autoCompleteEntryDto).ShowSuggestions();
    }

    public void HideSuggestions()
    {
        new DisplayAutoCompleteHandler(_autoCompleteEntryDto).HideSuggestions();
    }


    private async Task HandleTextCompleted()
    {
        await _textHandler.GetWordsFromTextRequested();
        _textHandler.AddWordsToTextRequested();

        if (_textDto.Invalid)
        {
            DeleteWordFromEntry();
            HideSuggestions();
            _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
            return;
        }

        
        var activeEntry = _autoCompleteEntryDto.IndexOfSelectedEntry;
        _autoCompleteEntryDto.XNameEntries[activeEntry].Text = _textDto.TextRequested;
        _autoCompleteEntryDto.Text = _textDto.TextRequested;

        foreach (var wordDto in _textDto.Words)
        {
            var itemView = new ItemViewModel() { Id = 0, IdParents = 0, Item = wordDto.WordRequested };
            var itemDto = new ItemDtoModel() { Id = 0, IdParents = 0, Item = wordDto.WordRequested };
            AddToElementToDisplay(itemView);
            AddToElementToAdd(itemDto);
        }

        HideSuggestions();
        _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
    }


    private string GetLastWord()
    {
        return _textHandler.GetLastWord();
    }

    private void DeleteWordFromEntry()
    {
        _textHandler.AddWordsToTextRequested();
        var activeEntry = _autoCompleteEntryDto.IndexOfSelectedEntry;
        _autoCompleteEntryDto.XNameEntries[activeEntry].Text = _textDto.TextRequested;

        _autoCompleteEntryDto.NewElementsToAdd.Clear();
        _autoCompleteEntryDto.ElementsToDisplay.Clear();
        foreach (var wordDto in _textDto.Words)
        {
            var itemView = new ItemViewModel(){Id = 0, IdParents = 0, Item = wordDto.WordRequested};
            var itemDto = new ItemDtoModel(){Id = 0, IdParents = 0, Item = wordDto.WordRequested};
            AddToElementToAdd(itemDto);
            AddToElementToDisplay(itemView);
        }
        
    }

    private void AddToElementToAdd(ItemDtoModel itemDto)
    {
        var isExisting = _autoCompleteEntryDto.NewElementsToAdd
            .Select(el => el.Item.ToLower().Equals(itemDto.Item.ToLower())).LastOrDefault(el => el.Equals(true));
        if (!isExisting)
        {
            _autoCompleteEntryDto.NewElementsToAdd.Add(new ItemDtoModel() { Item = itemDto.Item});
        }
    }


    private void AddToElementToDisplay(ItemViewModel itemView)
    {
        _autoCompleteEntryDto.ElementsToDisplay ??= new ObservableCollection<ItemViewModel>();

        var isExisting = _autoCompleteEntryDto.ElementsToDisplay
            .Select(el => el.Item.ToLower().Equals(itemView.Item.ToLower())).LastOrDefault(el => el.Equals(true));
        if (!isExisting)
            _autoCompleteEntryDto.ElementsToDisplay.Add(new ItemViewModel() { Id = itemView.Id, IdParents = itemView.IdParents, Item = itemView.Item});
    }

}