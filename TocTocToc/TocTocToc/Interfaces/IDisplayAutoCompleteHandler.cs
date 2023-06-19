namespace TocTocToc.Interfaces;

public interface IDisplayAutoCompleteHandler
{
    void ShowSuggestions();
    void HideSuggestions();
    int FindFocus();
}