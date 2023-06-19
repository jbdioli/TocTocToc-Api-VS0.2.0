using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class CountyViewModel
{
    public int Id { get; set; }
    public int IdStates { get; set; }
    public string County { get; set; }

}