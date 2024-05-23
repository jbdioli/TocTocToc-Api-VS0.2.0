using System;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;

namespace TocTocToc.Shared;

public class DisplayAutoCompleteHandler: IDisplayAutoCompleteHandler
{
    private readonly AutoCompleteEntryModel _autoCompleteEntry;


    protected DisplayAutoCompleteHandler()
    {

    }

    public DisplayAutoCompleteHandler(AutoCompleteEntryModel autoCompleteEntry)
    {
        _autoCompleteEntry = autoCompleteEntry;

    }


    public void ShowSuggestions()
    {
        if (_autoCompleteEntry == null)
            throw new ArgumentNullException( nameof(_autoCompleteEntry), "[ERROR] - AutoCompleteEntryModel null in class DisplayAutoCompleteHandler");

        _autoCompleteEntry.IsSuggestionView = true;
    }


    public void HideSuggestions()
    {
        _autoCompleteEntry.IsSuggestionView = false;
    }
}