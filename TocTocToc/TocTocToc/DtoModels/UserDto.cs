using Newtonsoft.Json;
using System.Collections.Generic;

namespace TocTocToc.DtoModels
{
    public class UserDto
    {
        public UserDto() {}

        public UserDto(AddressDto[] addresses)
        {
            Addresses = addresses;
        }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("pseudo")]
        public string Pseudo { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("languages")]
        public string Languages { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("genders")]
        public IList<GenderDto> Genders { get; set; }

        [JsonProperty("idGenders")]
        public int IdGenders { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("maritalStatuses")]
        public IList<MaritalStatusDto> MaritalStatuses { get; set; }

        [JsonProperty("idMaritalStatus")]
        public int IdMaritalStatus { get; set; }

        [JsonProperty("maritalStatus")]
        public string MaritalStatus { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("interests")]
        public string Interests { get; set; }

        [JsonProperty("addresses")]
        public AddressDto[] Addresses { get; set; }

        [JsonProperty("isAge")]
        public  bool IsAge { get; set; }

        [JsonProperty("isFloor")]
        public bool IsFloor { get; set; }

        [JsonProperty("isStatus")]
        public bool IsStatus { get; set; }

        [JsonProperty("isJob")]
        public bool IsJob { get; set; }


        [JsonProperty("isApartmentNumber")]
        public bool IsApartmentNumber { get; set; }
    }
}
