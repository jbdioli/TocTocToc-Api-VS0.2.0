using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class CityViewModel
{
    public int Id { get; set; }
    public int IdCounties { get; set; }
    public string City { get; set; }
    public double Lon { get; set; }
    public double Lat { get; set; }

}