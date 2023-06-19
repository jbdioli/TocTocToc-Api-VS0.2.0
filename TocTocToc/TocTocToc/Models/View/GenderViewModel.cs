using PropertyChanged;

namespace TocTocToc.Models.View
{

    [AddINotifyPropertyChangedInterface]
    public class GenderViewModel
    {

        public GenderViewModel()
        {
            IsMaleValid = true;
            IsFemaleValid = false;
        }

        public int Id { get; set; }
        public string Gender { get; set; }

        public bool IsMaleValid { get; set; }
        public bool IsFemaleValid { get; set; }
    }
}