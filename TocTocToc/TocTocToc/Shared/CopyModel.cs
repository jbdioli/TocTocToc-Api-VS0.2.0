using System.Collections.Generic;
using System.Globalization;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;

namespace TocTocToc.Shared;

public static class CopyModel
{
    public static void UserCopyViewModelToDto(UserViewModel userViewModel, UserDtoModel userDto)
    {
        userDto.UserId = userViewModel.UserId;
        userDto.Firstname = userViewModel.Firstname;
        userDto.Lastname = userViewModel.Lastname;
        userDto.Pseudo = userViewModel.Pseudo;
        userDto.Email = userViewModel.Email;
        userDto.PhoneNumber = userViewModel.PhoneNumber;
        userDto.Birthday = userViewModel.Birthday;
        userDto.Languages = userViewModel.Languages;
        userDto.Company = userViewModel.Company;
        userDto.Path = userViewModel.Path;
        userDto.Photo = userViewModel.Photo;
        userDto.IdGenders = userViewModel.IdGenders;
        userDto.IdMaritalStatus = userViewModel.IdMaritalStatus;
        userDto.Job = userViewModel.Job;
        userDto.Interests = userViewModel.Interests;
        userDto.IsAge = userViewModel.IsAge;
        userDto.IsFloor = userViewModel.IsFloor;
        userDto.IsStatus = userViewModel.IsStatus;
        userDto.IsJob = userViewModel.IsJob;
        userDto.IsApartmentNumber = userViewModel.IsApartmentNumber;
    }

    public static void UserCopyDtoToViewModel(UserDtoModel userDto, UserViewModel userViewModel)
    {
        userViewModel.UserId = userDto.UserId;
        userViewModel.Firstname = userDto.Firstname;
        userViewModel.Lastname = userDto.Lastname;
        userViewModel.Birthday = userDto.Birthday;
        userViewModel.Company = userDto.Company;
        userViewModel.Email = userDto.Email;
        userViewModel.PhoneNumber = userDto.PhoneNumber;
        userViewModel.IdGenders = userDto.IdGenders;
        userViewModel.Gender = userDto.Gender;
        userViewModel.IdMaritalStatus = userDto.IdMaritalStatus;
        userViewModel.MaritalStatus = userDto.MaritalStatus;
        userViewModel.Interests = userDto.Interests;
        userViewModel.IsAge = userDto.IsAge;
        userViewModel.IsApartmentNumber = userDto.IsApartmentNumber;
        userViewModel.IsFloor = userDto.IsFloor;
        userViewModel.IsJob = userDto.IsJob;
        userViewModel.IsStatus = userDto.IsStatus;
        userViewModel.Photo = userDto.Photo;
        userViewModel.Path = userDto.Path;
        userViewModel.Pseudo = userDto.Pseudo;
    }

    public static void AddressCopyDtoToViewModel(AddressDtoModel addressDto, AddressViewModel addressViewModel)
    {
        addressViewModel.AddressId = addressDto.AddressId;
        addressViewModel.Title = addressDto.Title;
        addressViewModel.IdHousingTypes = addressDto.IdHousingTypes;
        addressViewModel.Address = addressDto.Address;
        addressViewModel.Address1 = addressDto.Address1;
        addressViewModel.Address2 = addressDto.Address2;
        addressViewModel.StreetNumber = addressDto.StreetNumber;
        addressViewModel.ResidenceName = addressDto.ResidenceName;
        addressViewModel.BuildingNumber = addressDto.BuildingNumber;
        addressViewModel.BuildingName = addressDto.BuildingName;
        addressViewModel.BuildingEntrance = addressDto.BuildingEntrance;
        addressViewModel.Floor = addressDto.Floor.ToString();
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
        addressViewModel.IsActive = addressDto.IsActive;
    }


    public static void AddressCopyViewModelToDto(AddressViewModel addressViewModel, AddressDtoModel addressDto)
    {
        addressDto.AddressId = addressViewModel.AddressId;
        addressDto.Title = addressViewModel.Title;
        addressDto.IdHousingTypes = addressViewModel.IdHousingTypes;
        addressDto.Address = addressViewModel.Address;
        addressDto.Address1 = addressViewModel.Address1;
        addressDto.Address2 = addressViewModel.Address2;
        addressDto.StreetNumber = addressViewModel.StreetNumber;
        addressDto.ResidenceName = addressViewModel.ResidenceName;
        addressDto.BuildingNumber = addressViewModel.BuildingNumber;
        addressDto.BuildingName = addressViewModel.BuildingName;
        addressDto.BuildingEntrance = addressViewModel.BuildingEntrance;
        addressDto.Floor = !string.IsNullOrEmpty(addressViewModel.Floor) ? int.Parse(addressViewModel.Floor) : 0;
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
        addressDto.IsActive = addressViewModel.IsActive;
    }


    /*
     * Advertising model copy
     */
    public static void AdvertisingCopyDtoToViewModel(AdvertisingDtoModel adDto, AdvertisingViewModel adView)
    {
        adView.Area ??= new AreaSelectedViewModel();
        adView.InterestsDetails ??= new List<InterestViewModel>();

        adView.AdvertisingId = adDto.AdvertisingId;
        adView.Image = adDto.Image;
        adView.Path = adDto.Path;
        adView.Name = adDto.Name;
        adView.Info = adDto.Info;

        AreaCopyDtoToViewModel(adDto.Area, adView.Area);

        adView.IdGenders = adDto.IdGender;
        adView.Gender = adDto.Gender;
        adView.IsAllAge = adDto.IsAllAge;
        adView.AgeMini = adDto.AgeMini.ToString();
        adView.AgeMaxi = adDto.AgeMaxi.ToString();
        adView.Interests = adDto.Interests;

        if (adDto.InterestsDetails != null)
        {
            foreach (var interestDetailsDto in adDto.InterestsDetails)
            {
                var interestView = new InterestViewModel();
                InterestCopyDtoToViewModel(interestDetailsDto, interestView);
                adView.InterestsDetails.Add(interestView);
            }
        }
        
        adView.Date = adDto.Date;
        adView.IsPause = adDto.IsPause;
        adView.Duration = adDto.Duration.ToString();
        adView.Budget = adDto.Budget.ToString();
        adView.IsPayed = adDto.IsPayed;
        adView.IsImage = adDto.IsImage;
        adView.IsEditMode = adDto.IsEditMode;
    }



    public static void AdvertisingCopyViewModelToDto(AdvertisingViewModel adView, AdvertisingDtoModel adDto)
    {

        adDto.Area ??= new AreaSelectedDtoModel();
        adDto.InterestsDetails ??= new List<InterestDtoModel>();

        adDto.AdvertisingId = adView.AdvertisingId;
        adDto.Image = adView.Image;
        adDto.Path = adView.Path;
        adDto.Name = adView.Name;
        adDto.Info = adView.Info;

        AreaCopyViewModelToDto(adView.Area, adDto.Area);

        adDto.IdGender = adView.IdGenders;
        adDto.Gender = adView.Gender;
        adDto.Interests = adView.Interests;

        if (adView.InterestsDetails != null)
        {
            foreach (var interestView in adView.InterestsDetails)
            {
                var interestDto = new InterestDtoModel();
                InterestCopyViewModelToDto(interestView, interestDto);
                adDto.InterestsDetails.Add(interestDto);
            }
        }

        adDto.IsAllAge = adView.IsAllAge;
        adDto.AgeMini = !string.IsNullOrEmpty(adView.AgeMini) ? int.Parse(adView.AgeMini) : 0;
        adDto.AgeMaxi = !string.IsNullOrEmpty(adView.AgeMaxi) ? int.Parse(adView.AgeMaxi) : 0;
        adDto.Date = adView.Date;
        adDto.IsPause = adView.IsPause;
        adDto.Duration = !string.IsNullOrEmpty(adView.Duration) ? int.Parse(adView.Duration) : 0;
        adDto.Budget = !string.IsNullOrEmpty(adView.Budget) ? int.Parse(adView.Budget) : 0;
        adDto.IsPayed = adView.IsPayed;
    }



    /*
     * Interest model copy
    */
    public static void InterestCopyDtoToViewModel(InterestDtoModel interestDto, InterestViewModel interestView)
    {
        interestView.Id = interestDto.Id;
        interestView.Interest = interestDto.Interest;
    }



    public static void InterestCopyViewModelToDto(InterestViewModel interestView, InterestDtoModel interestDto)
    {
        interestDto.Id = interestView.Id;
        interestDto.Interest = interestView.Interest;
    }



    /*
     * Area model copy
     */


    public static void AreaCopyDtoToViewModel(AreaSelectedDtoModel areaDto, AreaSelectedViewModel areaView)
    {
        areaDto ??= new AreaSelectedDtoModel();
        
        areaView.CountrySelected ??= new CountryViewModel();
        areaView.StatesSelected ??= new List<StateViewModel>();
        areaView.CountiesSelected ??= new List<CountyViewModel>();
        areaView.CitiesSelected ??= new List<CityViewModel>();

        CountryCopyDtoToViewModel(areaDto.CountrySelected, areaView.CountrySelected);
        
        foreach (var stateDto in areaDto.StatesSelected)
        {
            var stateView = new StateViewModel();
            StateCopyDtoToViewModel(stateDto, stateView);
            areaView.StatesSelected.Add(stateView);
        }

        foreach (var countyDto in areaDto.CountiesSelected)
        {
            var countyView = new CountyViewModel();
            CountyCopyDtoToViewModel(countyDto, countyView);
            areaView.CountiesSelected.Add(countyView);
        }

        foreach (var cityDto in areaDto.CitiesSelected)
        {
            var cityView = new CityViewModel();
            CityCopyDtoToViewModel(cityDto, cityView);
            areaView.CitiesSelected.Add(cityView);
        }

        areaView.IsAllCountry = areaDto.IsAllCountry;
        areaView.IsAllState = areaDto.IsAllState;
        areaView.IsAllCounty = areaDto.IsAllCounty;
        areaView.IsAllCity = areaDto.IsAllCity;

        areaView.Km = areaDto.Km;
    }

    public static void AreaCopyViewModelToDto(AreaSelectedViewModel areaView, AreaSelectedDtoModel areaDto)
    {
        areaView ??= new AreaSelectedViewModel();

        areaDto.CountrySelected ??= new CountryDtoModel();
        areaDto.StatesSelected ??= new List<StateDtoModel>();
        areaDto.CountiesSelected ??= new List<CountyDtoModel>();
        areaDto.CitiesSelected ??= new List<CityDtoModel>();


        CountryCopyViewModelToDto(areaView.CountrySelected, areaDto.CountrySelected);

        foreach (var stateView in areaView.StatesSelected)
        {
            var stateDto = new StateDtoModel();
            StateCopyViewModelToDto(stateView, stateDto);
            areaDto.StatesSelected.Add(stateDto);
        }

        foreach (var countyView in areaView.CountiesSelected)
        {
            var countyDto = new CountyDtoModel();
            CountyCopyViewModelToDto(countyView, countyDto);
            areaDto.CountiesSelected.Add(countyDto);
        }

        foreach (var cityView in areaView.CitiesSelected)
        {
            var cityDto = new CityDtoModel();
            CityCopyViewModelToDto(cityView, cityDto);
            areaDto.CitiesSelected.Add(cityDto);
        }

        areaDto.IsAllCountry = areaView.IsAllCountry;
        areaDto.IsAllState = areaView.IsAllState;
        areaDto.IsAllCounty = areaView.IsAllCounty;
        areaDto.IsAllCity = areaView.IsAllCity;

        areaDto.Km = areaDto.Km;
    }


    /*
     * Country model copy
     */
    
    
    public static void CountryCopyDtoToViewModel(CountryDtoModel countryDto, CountryViewModel countryView)
    {
        countryDto ??= new CountryDtoModel();

        countryView.Id  = countryDto.Id;
        countryView.Country = countryDto.Country;
        countryView.IsoAlpha2 = countryDto.IsoAlpha2;
        countryView.PhoneCode = countryDto.PhoneCode;
    }


    public static void CountryCopyViewModelToDto(CountryViewModel countryView, CountryDtoModel countryDto)
    {
        countryView ??= new CountryViewModel();

        countryDto.Id = countryView.Id;
        countryDto.Country = countryView.Country;
        countryDto.IsoAlpha2 = countryView.IsoAlpha2;
        countryDto.PhoneCode = countryView.PhoneCode;
    }


    /*
     * State model copy
     */
    
    
    public static void StateCopyDtoToViewModel(StateDtoModel stateDto, StateViewModel stateView)
    {
        stateDto ??= new StateDtoModel();

        stateView.Id = stateDto.Id;
        stateView.IdCountries = stateDto.IdCountries;
        stateView.State = stateDto.State;
    }


    public static void StateCopyViewModelToDto(StateViewModel stateView, StateDtoModel stateDto)
    {
        stateView ??= new StateViewModel();

        stateDto.Id = stateView.Id;
        stateDto.IdCountries = stateView.IdCountries;
        stateDto.State = stateView.State;
    }


    /*
     * County model copy
     */
    
    
    public static void CountyCopyDtoToViewModel(CountyDtoModel countyDto, CountyViewModel countyView)
    {
        countyDto ??= new CountyDtoModel();

        countyView.Id = countyDto.Id;
        countyView.IdStates = countyDto.IdStates;
        countyView.County = countyDto.County;
    }


    public static void CountyCopyViewModelToDto(CountyViewModel countyView, CountyDtoModel countyDto)
    {
        countyView ??= new CountyViewModel();

        countyDto.Id = countyView.Id;
        countyDto.IdStates = countyView.IdStates;
        countyDto.County = countyView.County;
    }


    /*
     * City model copy
     */
    
    
    public static void CityCopyDtoToViewModel(CityDtoModel cityDto, CityViewModel cityView)
    {
        cityDto ??= new CityDtoModel();

        cityView.Id = cityDto.Id;
        cityView.IdCounties = cityDto.IdCounties;
        cityView.City = cityDto.City;
        cityView.Lat = cityDto.Lat;
        cityView.Lon = cityDto.Lon;
    }


    public static void CityCopyViewModelToDto(CityViewModel cityView, CityDtoModel cityDto)
    {
        cityDto ??= new CityDtoModel();

        cityDto.Id = cityView.Id;
        cityDto.IdCounties = cityView.IdCounties;
        cityDto.City = cityView.City;
        cityDto.Lat = cityView.Lat;
        cityDto.Lon = cityView.Lon;
    }


    /*
     *EPay Budget model copy
     */
    public static void EPayBudgetViewToDto(BudgetViewModel ePayBudgetView, BudgetDtoModel ePayBudgetDto)
    {

        ePayBudgetDto.Budget = !string.IsNullOrEmpty(ePayBudgetView.Budget) ? double.Parse(ePayBudgetView.Budget) : 1;
        ePayBudgetDto.StartDate = ePayBudgetView.StartDate;
        ePayBudgetDto.EndDate = ePayBudgetView.EndDate;
        ePayBudgetDto.Duration = !string.IsNullOrEmpty(ePayBudgetView.Duration) ? int.Parse(ePayBudgetView.Duration) : 1;
    }

    public static void EPayBudgetDtoToView(BudgetDtoModel ePayBudgetDto, BudgetViewModel ePayBudgetView)
    {
        ePayBudgetView.Budget = ePayBudgetDto.Budget.ToString(CultureInfo.InvariantCulture);
        ePayBudgetView.StartDate = ePayBudgetDto.StartDate;
        ePayBudgetView.EndDate = ePayBudgetDto.EndDate;
        ePayBudgetView.Duration = ePayBudgetDto.Duration.ToString();
    }

    /*
     * EPay Payment model copy
     */
    public static void EPayPaymentCopyViewModelToDto(EPayPaymentViewModel ePayPaymentView, EPayPaymentDtoModel ePayPaymentDto)
    {
        ePayPaymentDto.CardNo = ePayPaymentView.CardNo;
        ePayPaymentDto.ExpMonth = ePayPaymentView.ExpMonth;
        ePayPaymentDto.ExpYear = ePayPaymentView.ExpYear;
        ePayPaymentDto.CardCvv = ePayPaymentView.CardCvv;
        ePayPaymentDto.Amount = !string.IsNullOrEmpty(ePayPaymentView.Amount) ? int.Parse(ePayPaymentView.Amount) : 0;
        ePayPaymentDto.Currency = ePayPaymentView.Currency;
        ePayPaymentDto.Description = ePayPaymentView.Description;
        ePayPaymentDto.IsPayed = ePayPaymentView.IsPayed;
    }


    public static void EPayPaymentCopyDtoToViewModel(EPayPaymentDtoModel ePayPaymentDto, EPayPaymentViewModel ePayPaymentView)
    {
        ePayPaymentView.CardNo = ePayPaymentDto.CardNo;
        ePayPaymentView.ExpMonth = ePayPaymentDto.ExpMonth;
        ePayPaymentView.ExpYear = ePayPaymentDto.ExpYear;
        ePayPaymentView.CardCvv = ePayPaymentDto.CardCvv;
        ePayPaymentView.Amount = ePayPaymentDto.Amount.ToString();
        ePayPaymentView.Currency = ePayPaymentDto.Currency;
        ePayPaymentView.Description = ePayPaymentDto.Description;
        ePayPaymentView.IsPayed = ePayPaymentDto.IsPayed;
    }

    /*
     * EPay Order line model copy
     */
    public static void EPayOrderLinesCopyViewModelToDto(EPayOrderLinesViewModel ePayOrderLinesView, EPayOrderLinesDtoModel ePayOrderLinesDto)
    {
        ePayOrderLinesDto.IdOrders = ePayOrderLinesView.IdOrders;
        ePayOrderLinesDto.ProductRef = ePayOrderLinesView.ProductRef;
        ePayOrderLinesDto.Wording = ePayOrderLinesView.Wording;
        ePayOrderLinesDto.Quantity = ePayOrderLinesView.Quantity;
        ePayOrderLinesDto.PriceUnitHt = ePayOrderLinesView.PriceUnitHt;
        ePayOrderLinesDto.PriceUnitTtc = ePayOrderLinesView.PriceUnitTtc;
        ePayOrderLinesDto.PriceBaseHt = ePayOrderLinesView.PriceBaseHt;
        ePayOrderLinesDto.PriceBaseTtc = ePayOrderLinesView.PriceBaseTtc;
        ePayOrderLinesDto.TvaRate = ePayOrderLinesView.TvaRate;
        ePayOrderLinesDto.DiscountPercent = ePayOrderLinesView.DiscountPercent;
    }

    public static void EPayOrderLinesCopyDtoToViewModel(EPayOrderLinesDtoModel ePayOrderLinesDto, EPayOrderLinesViewModel ePayOrderLinesView)
    {
        ePayOrderLinesView.IdOrders = ePayOrderLinesDto.IdOrders;
        ePayOrderLinesView.ProductRef = ePayOrderLinesDto.ProductRef;
        ePayOrderLinesView.Wording = ePayOrderLinesDto.Wording;
        ePayOrderLinesView.Quantity = ePayOrderLinesDto.Quantity;
        ePayOrderLinesView.PriceUnitHt = ePayOrderLinesDto.PriceUnitHt;
        ePayOrderLinesView.PriceUnitTtc = ePayOrderLinesDto.PriceUnitTtc;
        ePayOrderLinesView.PriceBaseHt = ePayOrderLinesDto.PriceBaseHt;
        ePayOrderLinesView.PriceBaseTtc = ePayOrderLinesDto.PriceBaseTtc;
        ePayOrderLinesView.TvaRate = ePayOrderLinesDto.TvaRate;
        ePayOrderLinesView.DiscountPercent = ePayOrderLinesDto.DiscountPercent;
    }

    /*
     * EPay Address model copy
     */
    public static void EPayAddressCopyViewModelToDto(EPayAddressViewModel ePayAddressView, EPayAddressDtoModel ePayAddressDto)
    {
        ePayAddressDto.Id = ePayAddressView.Id;
        ePayAddressDto.Firstname = ePayAddressView.Firstname;
        ePayAddressDto.Lastname = ePayAddressView.Lastname;
        ePayAddressDto.PhoneNumber = ePayAddressView.PhoneNumber;
        ePayAddressDto.Email = ePayAddressView.Email;
        ePayAddressDto.Company = ePayAddressView.Company;
        ePayAddressDto.Address1 = ePayAddressView.Address1;
        ePayAddressDto.Address2 = ePayAddressView.Address2;
        ePayAddressDto.Address3 = ePayAddressView.Address3;
        ePayAddressDto.IdCities = ePayAddressView.IdCities;
        ePayAddressDto.City = ePayAddressView.City;
        ePayAddressDto.Zipcode = ePayAddressView.Zipcode;
        ePayAddressDto.IdCountries = ePayAddressView.IdCountries;
        ePayAddressDto.Country = ePayAddressView.Country;
    }

    public static void EPayAddressCopyDtoToViewModel(EPayAddressDtoModel ePayAddressDto, EPayAddressViewModel ePayAddressView)
    {
        ePayAddressView.Id = ePayAddressDto.Id;
        ePayAddressView.Firstname = ePayAddressDto.Firstname;
        ePayAddressView.Lastname = ePayAddressDto.Lastname;
        ePayAddressView.PhoneNumber = ePayAddressDto.PhoneNumber;
        ePayAddressView.Email = ePayAddressDto.Email;
        ePayAddressView.Company = ePayAddressDto.Company;
        ePayAddressView.Address1 = ePayAddressDto.Address1;
        ePayAddressView.Address2 = ePayAddressDto.Address2;
        ePayAddressView.Address3 = ePayAddressDto.Address3;
        ePayAddressView.IdCities = ePayAddressDto.IdCities;
        ePayAddressView.City = ePayAddressDto.City;
        ePayAddressView.Zipcode = ePayAddressDto.Zipcode;
        ePayAddressView.IdCountries = ePayAddressDto.IdCountries;
        ePayAddressView.Country = ePayAddressDto.Country;
    }


    /*
     * EPay Order model copy
     */
    public static void EPayOrderCopyViewModelToDto(EPayOrderViewModel ePayOrderView, EPayOrderDtoModel ePayOrderDto)
    {
        ePayOrderDto.Id = ePayOrderView.Id;
        ePayOrderDto.OrderId = ePayOrderView.OrderId;
        ePayOrderDto.IdUsers = ePayOrderView.IdUsers;
        ePayOrderDto.IdBillAddress = ePayOrderView.IdBillAddress;
        ePayOrderDto.IdShipAddress = ePayOrderView.IdShipAddress;
        ePayOrderDto.Reference = ePayOrderView.Reference;
        ePayOrderDto.OrderState = ePayOrderView.OrderState;
        ePayOrderDto.InvoiceNum = ePayOrderView.InvoiceNum;
        ePayOrderDto.AmountTotalHt = ePayOrderView.AmountTotalHt;
        ePayOrderDto.AmountTotalTtc = ePayOrderView.AmountTotalTtc;
        ePayOrderDto.AmountTotalTva = ePayOrderView.AmountTotalTva;
        ePayOrderDto.PortPriceTtc = ePayOrderView.PortPriceTtc;
        ePayOrderDto.AmountToPay = ePayOrderView.AmountToPay;
        ePayOrderDto.DateInvoice = ePayOrderView.DateInvoice;
        ePayOrderDto.DateSendOrder = ePayOrderView.DateSendOrder;
        ePayOrderDto.DateSendInvoice = ePayOrderView.DateSendInvoice;

        foreach (var orderLine in ePayOrderView.OrderLines)
        {
            var oderLineDto = new EPayOrderLinesDtoModel();
            EPayOrderLinesCopyViewModelToDto(orderLine, oderLineDto);
            ePayOrderDto.OrderLines.Add(oderLineDto);
        }

        EPayAddressCopyViewModelToDto(ePayOrderView.BillingAddress, ePayOrderDto.BillingAddress);
        
        if (ePayOrderView.ShippingAddress != null || !ePayOrderView.IsShippingAsBilling)
            EPayAddressCopyViewModelToDto(ePayOrderView.ShippingAddress, ePayOrderDto.ShippingAddress);

    }


    public static void EPayOrderCopyDtoToViewModel(EPayOrderDtoModel ePayOrderDto, EPayOrderViewModel ePayOrderView)
    {
        ePayOrderView.Id = ePayOrderDto.Id;
        ePayOrderView.OrderId = ePayOrderDto.OrderId;
        ePayOrderView.IdUsers = ePayOrderDto.IdUsers;
        ePayOrderView.IdBillAddress = ePayOrderDto.IdBillAddress;
        ePayOrderView.IdShipAddress = ePayOrderDto.IdShipAddress;
        ePayOrderView.Reference = ePayOrderDto.Reference;
        ePayOrderView.OrderState = ePayOrderDto.OrderState;
        ePayOrderView.InvoiceNum = ePayOrderDto.InvoiceNum;
        ePayOrderView.AmountTotalHt = ePayOrderDto.AmountTotalHt;
        ePayOrderView.AmountTotalTtc = ePayOrderDto.AmountTotalTtc;
        ePayOrderView.AmountTotalTva = ePayOrderDto.AmountTotalTva;
        ePayOrderView.PortPriceTtc = ePayOrderDto.PortPriceTtc;
        ePayOrderView.AmountToPay = ePayOrderDto.AmountToPay;
        ePayOrderView.DateInvoice = ePayOrderDto.DateInvoice;
        ePayOrderView.DateSendOrder = ePayOrderDto.DateSendOrder;
        ePayOrderView.DateSendInvoice = ePayOrderDto.DateSendInvoice;

        foreach (var orderLine in ePayOrderDto.OrderLines)
        {
            var oderLineView = new EPayOrderLinesViewModel();
            EPayOrderLinesCopyDtoToViewModel(orderLine, oderLineView);
            ePayOrderView.OrderLines.Add(oderLineView);
        }

        EPayAddressCopyDtoToViewModel(ePayOrderDto.BillingAddress, ePayOrderView.BillingAddress);
        
        if (ePayOrderDto.ShippingAddress != null)
            EPayAddressCopyDtoToViewModel(ePayOrderDto.ShippingAddress, ePayOrderView.ShippingAddress);
    }

    /*
     * EPay Details model copy
     */
    public static void EPayDetailsCopyViewModelToDto(EPayDetailsViewModel ePayDetailsView, EPayDetailsDtoModel ePayDetailsDto)
    {
        ePayDetailsDto.IsEPayValid = ePayDetailsView.IsEPayValid;

        EPayPaymentCopyViewModelToDto(ePayDetailsView.EPayPayment, ePayDetailsDto.EPayPayment);
        EPayOrderCopyViewModelToDto(ePayDetailsView.EPayOrder, ePayDetailsDto.EPayOrder);
    }


    public static void EPayDetailsCopyDtoToViewModel(EPayDetailsDtoModel ePayDetailsDto, EPayDetailsViewModel ePayDetailsView)
    {
        ePayDetailsView.IsEPayValid = ePayDetailsDto.IsEPayValid;

        EPayPaymentCopyDtoToViewModel(ePayDetailsDto.EPayPayment, ePayDetailsView.EPayPayment);
        EPayOrderCopyDtoToViewModel(ePayDetailsDto.EPayOrder, ePayDetailsView.EPayOrder);

    }



    //public static void DuckCopyShallow(object dst, object src)
    //{
    //    var srcT = src.GetType();
    //    var dstT = dst.GetType();
    //    foreach (var f in srcT.GetFields())
    //    {
    //        var dstF = dstT.GetField(f.Name);
    //        if (dstF == null || dstF.IsLiteral)
    //            continue;
    //        dstF.SetValue(dst, f.GetValue(src));
    //    }

    //    foreach (var f in srcT.GetProperties())
    //    {
    //        var dstF = dstT.GetProperty(f.Name);
    //        if (dstF == null)
    //            continue;

    //        dstF.SetValue(dst, f.GetValue(src, null), null);
    //    }
    //}

}