
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class CityViewModel : ObservableObject
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