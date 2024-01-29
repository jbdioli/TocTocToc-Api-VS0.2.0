using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model
{
    public partial class SettingModel : BaseViewModel
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