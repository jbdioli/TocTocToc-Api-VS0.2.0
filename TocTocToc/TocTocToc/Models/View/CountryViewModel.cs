using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class CountryViewModel
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string IsoAlpha2 { get; set; }
    public int PhoneCode { get; set; }

}