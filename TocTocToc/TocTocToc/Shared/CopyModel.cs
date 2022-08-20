using TocTocToc.DtoModels;
using TocTocToc.ViewModels;

namespace TocTocToc.Shared;

public class CopyModel
{
    public void AddressCopyDtoToViewModel(AddressDto addressDto, AddressViewModel addressViewModel)
    {
        addressViewModel.AddressId = addressDto.AddressId;
        addressViewModel.Title = addressDto.Title;
        addressViewModel.IdHousingTypes = addressDto.IdHousingTypes;
        addressViewModel.Address = addressDto.Address;
        addressViewModel.StreetNumber = addressDto.StreetNumber;
        addressViewModel.ResidenceName = addressDto.ResidenceName;
        addressViewModel.BuildingNumber = addressDto.BuildingNumber;
        addressViewModel.BuildingName = addressDto.BuildingName;
        addressViewModel.BuildingEntrance = addressDto.BuildingEntrance;
        addressViewModel.Floor = addressDto.Floor;
        addressViewModel.City = addressDto.City;
        addressViewModel.IdCities = addressDto.IdCities;
        addressViewModel.County = addressDto.County;
        addressViewModel.IdCounties = addressDto.IdCounties;
        addressViewModel.State = addressDto.State;
        addressViewModel.IdStates = addressDto.IdStates;
        addressViewModel.Country = addressDto.Country;
        addressViewModel.IdCountries = addressDto.IdCountries;
        addressViewModel.Zipcode = addressDto.Zipcode;
        addressViewModel.Lat = addressDto.Lat;
        addressViewModel.Lon = addressDto.Lon;
        addressViewModel.DistanceWanted = addressDto.DistanceWanted;
        addressViewModel.IsActived = addressDto.IsActived;
    }


    public void AddressCopyViewModelToDto(AddressViewModel addressViewModel, AddressDto addressDto)
    {
        addressDto.AddressId = addressViewModel.AddressId;
        addressDto.Title = addressViewModel.Title;
        addressDto.IdHousingTypes = addressViewModel.IdHousingTypes;
        addressDto.Address = addressViewModel.Address;
        addressDto.StreetNumber = addressViewModel.StreetNumber;
        addressDto.ResidenceName = addressViewModel.ResidenceName;
        addressDto.BuildingNumber = addressViewModel.BuildingNumber;
        addressDto.BuildingName = addressViewModel.BuildingName;
        addressDto.BuildingEntrance = addressViewModel.BuildingEntrance;
        addressDto.Floor = addressViewModel.Floor;
        addressDto.City = addressViewModel.City;
        addressDto.IdCities = addressViewModel.IdCities;
        addressDto.County = addressViewModel.County;
        addressDto.IdCounties = addressViewModel.IdCounties;
        addressDto.State = addressViewModel.State;
        addressDto.IdStates = addressViewModel.IdStates;
        addressDto.Country = addressViewModel.Country;
        addressDto.IdCountries = addressViewModel.IdCountries;
        addressDto.Zipcode = addressViewModel.Zipcode;
        addressDto.Lat = addressViewModel.Lat;
        addressDto.Lon = addressViewModel.Lon;
        addressDto.DistanceWanted = addressViewModel.DistanceWanted;
        addressDto.IsActived = addressViewModel.IsActived;
    }

    public void AdvertisingCopyDtoToViewModel(AdvertisingDto ad, AdvertisingViewModel adViewModel)
    {
        adViewModel.AdvertisingId = ad.AdvertisingId;
        adViewModel.Image = ad.Image;
        adViewModel.Path = ad.Path;
        adViewModel.Name = ad.Name;
        adViewModel.Info = ad.Info;
        adViewModel.Genders = ad.Genders;
        adViewModel.IdGenders = ad.IdGender;
        adViewModel.Gender = ad.Gender;
        adViewModel.AgeMini = ad.AgeMini;
        adViewModel.AgeMaxi = ad.AgeMaxi;
        adViewModel.Date = ad.Date;
        adViewModel.IsPause = ad.IsPause;
        adViewModel.Duration = ad.Duration;
        adViewModel.Budget = ad.Budget;
        adViewModel.IsPayed = ad.IsPayed;
    }

    public void AdvertisingCopyViewModelToDto(AdvertisingViewModel adViewModel, AdvertisingDto ad)
    {
        ad.AdvertisingId = adViewModel.AdvertisingId;
        ad.Image = adViewModel.Image;
        ad.Path = adViewModel.Path;
        ad.Name = adViewModel.Name;
        ad.Info = adViewModel.Info;
        ad.Genders = adViewModel.Genders;
        ad.IdGender = adViewModel.IdGenders;
        ad.Gender = adViewModel.Gender;
        ad.AgeMini = adViewModel.AgeMini;
        ad.AgeMaxi = adViewModel.AgeMaxi;
        ad.Date = adViewModel.Date;
        ad.IsPause = adViewModel.IsPause;
        ad.Duration = adViewModel.Duration;
        ad.Budget = adViewModel.Budget;
        ad.IsPayed = adViewModel.IsPayed;
    }
}