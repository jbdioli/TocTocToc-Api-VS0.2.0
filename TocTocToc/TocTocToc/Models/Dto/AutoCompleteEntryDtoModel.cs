using System.Collections.Generic;
using System.Collections.ObjectModel;
using TocTocToc.Models.View;
using Xamarin.Forms;

namespace TocTocToc.Models.Dto;

public class AutoCompleteEntryDtoModel
{

    public AutoCompleteEntryDtoModel()
    {
        EventOrder = new AutoCompleteEventDtoModel();
        NewElementsToAdd = new List<ItemDtoModel>();
        ElementsToDisplay = new ObservableCollection<ItemViewModel>();
        Suggestions = new ObservableCollection<ItemViewModel>();
    }

    public string Text { get; set; }
    public ObservableCollection<ItemViewModel> Suggestions { get; set; }
    public ObservableCollection<ItemViewModel> ElementsToDisplay { get; set; }
    public List<ItemDtoModel> NewElementsToAdd { get; set; }
    public ListView XNameSuggestionView { get; set; }
    public List<Entry> XNameEntries { get; set; }
    public int IndexOfSelectedEntry { get; set; }
    public List<string> EndingCharactersEntry { get; set; }
    public AutoCompleteEventDtoModel EventOrder { get; set; }
}