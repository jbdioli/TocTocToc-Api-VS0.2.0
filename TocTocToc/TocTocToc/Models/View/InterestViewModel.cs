using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class InterestViewModel
{
    public int Id { get; set; }
    public string Interest { get; set; }
}