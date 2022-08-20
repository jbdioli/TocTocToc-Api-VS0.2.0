using System.Collections.Generic;
using PropertyChanged;
using TocTocToc.DtoModels;

namespace TocTocToc.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class UserViewModel : LanguageViewModel
    {
        public UserViewModel() { }

        public UserViewModel(AddressViewModel[] addresses)
        {
            Addresses = addresses;
        }

        public string UserId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Pseudo { get; set; }

        public string Email { get; set; }

        public string Birthday { get; set; }

        public string Company { get; set; }

        public IList<GenderDto> Genders { get; set; }

        public int IdGenders { get; set; }

        public string Gender { get; set; }

        public string Languages { get; set; }

        public IList<MaritalStatusDto> MaritalStatuses { get; set; }

        public int IdMaritalStatus { get; set; }

        public string MaritalStatus { get; set; }

        public string Path { get; set; }

        public string Photo { get; set; }
        
        public string Job { get; set; }

        public string Interests { get; set; }

        public bool IsFloor { get; set; }

        public bool IsAge { get; set; }

        public bool IsStatus { get; set; }

        public bool IsJob { get; set; }

        public bool IsApartmentNumber { get; set; }

        public AddressViewModel[] Addresses { get; set; }

        public string FullPathPhoto { get; set; }
    }
}