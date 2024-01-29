using System.Collections.Generic;
using System.Collections.ObjectModel;
using TocTocToc.Models.Model;

namespace TocTocToc.Models.Dto;

public class AutoCompleteEntryModel
{
    public string Text { get; set; }
    public ObservableCollection<ItemModel> ItemProposals { get; set; } = []; // All items from Data Base
    public ObservableCollection<ItemModel> ItemSuggestions { get; set; } = []; // All suggestion items form user taping
    public ObservableCollection<ItemModel> EntryItems { get; set; } = []; // All items from the entry

    public AutoCompleteEventModel EventOrder { get; set; } = new();
    public bool IsSuggestionView { get; set; }
}