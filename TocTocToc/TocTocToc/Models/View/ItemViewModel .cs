using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class ItemViewModel
{
    public int Id { get; set; }
    public int IdParents { get; set; }
    public string Item { get; set; }

}