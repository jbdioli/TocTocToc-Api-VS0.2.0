namespace TocTocToc.Models.Dto;

public class AutoCompleteEventDtoModel
{
    public bool ItemTappedDisable { get; set; } = false;
    public bool TextChangedDisable { get; set; } = false;
    public bool TextCompletedDisable { get; set; } = false;
    public bool UnfocusedDisable { get; set; } = false;
}