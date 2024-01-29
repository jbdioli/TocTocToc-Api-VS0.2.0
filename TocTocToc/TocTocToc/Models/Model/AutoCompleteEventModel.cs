namespace TocTocToc.Models.Model;

public class AutoCompleteEventModel
{
    public bool ItemTappedDisable { get; set; } = false;
    public bool TextChangedDisable { get; set; } = false;
    public bool TextCompletedDisable { get; set; } = false;
    public bool UnfocusedDisable { get; set; } = false;
}