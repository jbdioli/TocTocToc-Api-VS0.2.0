
using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model
{

    public partial class GenderModel : BaseViewModel
    {

        public GenderModel()
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