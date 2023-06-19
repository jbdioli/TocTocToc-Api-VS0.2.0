using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View
{
    public partial class SettingViewModel : ObservableObject
    {
        [ObservableProperty] 
        private string settingId;

        [ObservableProperty]
        private string language;

        [ObservableProperty]
        private bool isAge;

        [ObservableProperty]
        private bool isFloor;

        [ObservableProperty]
        private bool isStatus;

        [ObservableProperty]
        private bool isJob;
    }
}