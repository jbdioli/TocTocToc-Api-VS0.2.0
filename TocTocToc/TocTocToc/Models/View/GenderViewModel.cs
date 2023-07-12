
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View
{

    public partial class GenderViewModel : ObservableObject
    {

        public GenderViewModel()
        {
            _isMaleValid = true;
            _isFemaleValid = false;
        }

        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _gender;

        [ObservableProperty]
        private bool _isMaleValid;

        [ObservableProperty]
        private bool _isFemaleValid;
    }
}