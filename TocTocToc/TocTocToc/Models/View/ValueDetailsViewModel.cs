using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class ValueDetailsViewModel
{
    public string Text { get; set; }
}