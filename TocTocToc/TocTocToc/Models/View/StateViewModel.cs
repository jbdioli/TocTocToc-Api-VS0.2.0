using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class StateViewModel
{
    public int Id { get; set; }
    public int IdCountries { get; set; }
    public string State { get; set; }

}