using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TocTocToc.Models.Model;

namespace TocTocToc.Models.View;

public class AdvertisingViewModel : ObservableObject
{
    public ObservableCollection<AdvertisingModel> ObserverAdvertisingViewModels { get; set; } =
        new ObservableCollection<AdvertisingModel>();
}