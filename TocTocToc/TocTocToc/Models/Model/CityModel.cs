using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class CityModel : BaseViewModel
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private int _idCounties;

    [ObservableProperty]
    private string _city;

    [ObservableProperty]
    private double _lon;

    [ObservableProperty]
    private double _lat;

}