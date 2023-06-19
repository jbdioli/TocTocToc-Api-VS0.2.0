using System;
using System.Linq;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;

namespace TocTocToc.Shared;

public class DisplayAutoCompleteHandler: IDisplayAutoCompleteHandler
{
    private readonly AutoCompleteEntryDtoModel _autoCompleteEntryDto;


    protected DisplayAutoCompleteHandler()
    {

    }

    public DisplayAutoCompleteHandler(AutoCompleteEntryDtoModel autoCompleteEntryDto)
    {
        _autoCompleteEntryDto = autoCompleteEntryDto;

    }


    public void ShowSuggestions()
    {
        if (_autoCompleteEntryDto == null)
            throw new ArgumentNullException( nameof(_autoCompleteEntryDto), "[ERROR] - AutoCompleteEntryDtoModel null in class DisplayAutoCompleteHandler");

        _autoCompleteEntryDto.XNameSuggestionView.IsVisible = true;
        //_autoCompleteEntryDto.IndexOfSelectedEntry = _autoCompleteEntryDto.XNameEntries.FindIndex(e => e.IsFocused == true);
        _autoCompleteEntryDto.IndexOfSelectedEntry = FindFocus();
        foreach (var xNameEntry in from xNameEntry in _autoCompleteEntryDto.XNameEntries let isFocused = xNameEntry.IsFocused where !isFocused select xNameEntry)
        {
            xNameEntry.IsEnabled = false;
            
        }
    }


    public void HideSuggestions()
    {
        _autoCompleteEntryDto.XNameSuggestionView.IsVisible = false;
        foreach (var xNameEntry in _autoCompleteEntryDto.XNameEntries)
        {
            xNameEntry.IsEnabled = true;
        }
    }

    public int FindFocus()
    {
        if (_autoCompleteEntryDto?.XNameEntries == null) return -1;
        var index = _autoCompleteEntryDto.XNameEntries.FindIndex(e => e.IsFocused == true);
        return index;
    }
}