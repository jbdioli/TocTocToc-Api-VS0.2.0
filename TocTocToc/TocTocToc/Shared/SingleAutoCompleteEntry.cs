﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class SingleAutoCompleteEntry: IAutoCompeteEntry
{
    private readonly AutoCompleteEntryDtoModel _autoCompleteEntryDto;
    private readonly WordHandler _wordHandler;

    private readonly WordDtoModel _wordDto = new();

    private bool _isEditing = false;

    public SingleAutoCompleteEntry(AutoCompleteEntryDtoModel autoCompleteEntryDto)
    {
        _autoCompleteEntryDto = autoCompleteEntryDto;
        _wordHandler = new WordHandler(_wordDto);

        InitData();
    }


    private void InitData()
    {
        if (_autoCompleteEntryDto == null || _autoCompleteEntryDto != null && string.IsNullOrWhiteSpace(_autoCompleteEntryDto.Text)) return;
        var index = new DisplayAutoCompleteHandler(_autoCompleteEntryDto).FindFocus();
        _autoCompleteEntryDto.XNameEntries[index].Text = _autoCompleteEntryDto.Text;
        if (index < (_autoCompleteEntryDto.XNameEntries.Count -1))
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
        _autoCompleteEntryDto.XNameEntries[_autoCompleteEntryDto.IndexOfSelectedEntry].Text = itemView?.Item;
        if (itemView != null)
            _autoCompleteEntryDto.NewElementsToAdd.Add(new ItemDtoModel() { Id = itemView.Id, IdParents = itemView.IdParents, Item = itemView.Item });

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
            //var word = e.NewTextValue;
            _wordDto.WordRequested = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(_wordDto.WordRequested))
            {
                HideSuggestions();
                _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
                return;
            }

            var endingCharacter = IsNextOrEndingCharacter(_wordDto.WordRequested);
            if (endingCharacter)
            {
                //_wordDto.WordRequested = word;
                _wordHandler.CleaningWord();
                var isValidWord = await IsWordValid();
                if (!isValidWord)
                {
                    //SaveEntryData();
                    DeleteWordFromEntry();
                    HideSuggestions();
                    _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
                    return;
                }

                //SaveEntryData();
                _autoCompleteEntryDto.XNameEntries[_autoCompleteEntryDto.IndexOfSelectedEntry].Text = _wordDto.WordRequested;
                HideSuggestions();
                _autoCompleteEntryDto.XNameSuggestionView.EndRefresh();
                return;
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

        await HandleWordCompleted();
        HideSuggestions();

    }



    public bool IsNextOrEndingCharacter(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException("", "[ERROR] - In the method CheckNextOrEndingCharacter, Text can't be empty or null");
        }

        var lastCharacter = text.Substring(text.Length - 1);
        if (!lastCharacter.All(char.IsLetter))
        {
            var textBuffer = text.Trim();
            textBuffer = textBuffer.Remove(textBuffer.Length - 1, 1);
            var activeEntry = _autoCompleteEntryDto.IndexOfSelectedEntry;
            _autoCompleteEntryDto.XNameEntries[activeEntry].Text = textBuffer;
            return true;
        }

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
        }
        _autoCompleteEntryDto.XNameEntries[indexSelectedEntry].Text = _wordDto.WordRequested;
        _autoCompleteEntryDto.NewElementsToAdd.Add(new ItemDtoModel(){Id = 0, IdParents = 0, Item = _wordDto.WordRequested});
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

}
