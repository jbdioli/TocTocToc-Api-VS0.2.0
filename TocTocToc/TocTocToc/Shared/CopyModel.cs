using System.Collections.Generic;
using System.Globalization;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;

namespace TocTocToc.Shared;

public static class CopyModel
{

    /*
     * COPY User Model
     */

    public static void UserCopyModelToDto(UserModel userModel, UserDtoModel userDto)
    {
        userDto.UserId = userModel.UserId;
        userDto.Firstname = userModel.Firstname;
        userDto.Lastname = userModel.Lastname;
        userDto.Pseudo = userModel.Pseudo;
        userDto.Email = userModel.Email;
        userDto.PhoneNumber = userModel.PhoneNumber;
        userDto.Birthday = userModel.Birthday;
        userDto.Languages = userModel.Languages;
        userDto.Company = userModel.Company;
        userDto.Path = userModel.Path;
        userDto.Photo = userModel.Photo;
        userDto.IdGenders = userModel.IdGenders;
        userDto.IdMaritalStatus = userModel.IdMaritalStatus;
        userDto.Job = userModel.Job;
        userDto.Interests = userModel.Interests;
        userDto.IsAge = userModel.IsAge;
        userDto.IsFloor = userModel.IsFloor;
        userDto.IsStatus = userModel.IsStatus;
        userDto.IsJob = userModel.IsJob;
        userDto.IsApartmentNumber = userModel.IsApartmentNumber;
    }

    public static void UserCopyDtoToModel(UserDtoModel userDto, UserModel userModel)
    {
        userModel.UserId = userDto.UserId;
        userModel.Firstname = userDto.Firstname;
        userModel.Lastname = userDto.Lastname;
        userModel.Birthday = userDto.Birthday;
        userModel.Company = userDto.Company;
        userModel.Email = userDto.Email;
        userModel.PhoneNumber = userDto.PhoneNumber;
        userModel.IdGenders = userDto.IdGenders;
        userModel.Gender = userDto.Gender;
        userModel.IdMaritalStatus = userDto.IdMaritalStatus;
        userModel.MaritalStatus = userDto.MaritalStatus;
        userModel.Interests = userDto.Interests;
        userModel.IsAge = userDto.IsAge;
        userModel.IsApartmentNumber = userDto.IsApartmentNumber;
        userModel.IsFloor = userDto.IsFloor;
        userModel.IsJob = userDto.IsJob;
        userModel.IsStatus = userDto.IsStatus;
        userModel.Photo = userDto.Photo;
        userModel.Path = userDto.Path;
        userModel.Pseudo = userDto.Pseudo;
    }

    public static void AddressCopyDtoToModel(AddressDtoModel addressDto, AddressModel addressModel)
    {
        addressModel.AddressId = addressDto.AddressId;
        addressModel.Title = addressDto.Title;
        addressModel.IdHousingTypes = addressDto.IdHousingTypes;
        addressModel.Address = addressDto.Address;
        addressModel.Address1 = addressDto.Address1;
        addressModel.Address2 = addressDto.Address2;
        addressModel.StreetNumber = addressDto.StreetNumber;
        addressModel.ResidenceName = addressDto.ResidenceName;
        addressModel.BuildingNumber = addressDto.BuildingNumber;
        addressModel.BuildingName = addressDto.BuildingName;
        addressModel.BuildingEntrance = addressDto.BuildingEntrance;
        addressModel.Floor = addressDto.Floor.ToString();
        addressModel.City = addressDto.City;
        addressModel.IdCities = addressDto.IdCities;
        addressModel.County = addressDto.County;
        addressModel.IdCounties = addressDto.IdCounties;
        addressModel.State = addressDto.State;
        addressModel.IdStates = addressDto.IdStates;
        addressModel.Country = addressDto.Country;
        addressModel.IdCountries = addressDto.IdCountries;
        addressModel.Zipcode = addressDto.Zipcode;
        addressModel.Lat = addressDto.Lat;
        addressModel.Lon = addressDto.Lon;
        addressModel.DistanceWanted = addressDto.DistanceWanted;
        addressModel.IsActive = addressDto.IsActive;
    }


    /*
     * COPY Address Model
     */


    public static void AddressCopyModelToDto(AddressModel addressModel, AddressDtoModel addressDto)
    {
        addressDto.AddressId = addressModel.AddressId;
        addressDto.Title = addressModel.Title;
        addressDto.IdHousingTypes = addressModel.IdHousingTypes;
        addressDto.Address = addressModel.Address;
        addressDto.Address1 = addressModel.Address1;
        addressDto.Address2 = addressModel.Address2;
        addressDto.StreetNumber = addressModel.StreetNumber;
        addressDto.ResidenceName = addressModel.ResidenceName;
        addressDto.BuildingNumber = addressModel.BuildingNumber;
        addressDto.BuildingName = addressModel.BuildingName;
        addressDto.BuildingEntrance = addressModel.BuildingEntrance;
        addressDto.Floor = !string.IsNullOrEmpty(addressModel.Floor) ? int.Parse(addressModel.Floor) : 0;
        addressDto.City = addressModel.City;
        addressDto.IdCities = addressModel.IdCities;
        addressDto.County = addressModel.County;
        addressDto.IdCounties = addressModel.IdCounties;
        addressDto.State = addressModel.State;
        addressDto.IdStates = addressModel.IdStates;
        addressDto.Country = addressModel.Country;
        addressDto.IdCountries = addressModel.IdCountries;
        addressDto.Zipcode = addressModel.Zipcode;
        addressDto.Lat = addressModel.Lat;
        addressDto.Lon = addressModel.Lon;
        addressDto.DistanceWanted = addressModel.DistanceWanted;
        addressDto.IsActive = addressModel.IsActive;
    }


    /*
     * Advertising model copy
     */

    public static void AdvertisingCopyModelToDto(AdvertisingModel advertisingModel, AdvertisingDtoModel advertisingDto)
    {

        advertisingDto.Area ??= new AreaSelectedDtoModel();
        advertisingDto.InterestsDetails ??= new List<InterestDtoModel>();

        advertisingDto.AdvertisingId = advertisingModel.AdvertisingId;
        advertisingDto.Image = advertisingModel.Image;
        advertisingDto.Path = advertisingModel.Path;
        advertisingDto.Name = advertisingModel.Name;
        advertisingDto.Info = advertisingModel.Info;

        AreaCopyModelToDto(advertisingModel.Area, advertisingDto.Area);

        advertisingDto.IdGenders = advertisingModel.IdGenders;
        advertisingDto.Gender = advertisingModel.Gender;
        advertisingDto.Interests = advertisingModel.Interests;

        if (advertisingModel.InterestsDetails != null)
        {
            foreach (var interestModel in advertisingModel.InterestsDetails)
            {
                var interestDto = new InterestDtoModel();
                InterestCopyModelToDto(interestModel, interestDto);
                advertisingDto.InterestsDetails.Add(interestDto);
            }
        }

        advertisingDto.IsAllAge = advertisingModel.IsAllAge;
        advertisingDto.AgeMini = !string.IsNullOrEmpty(advertisingModel.AgeMini) ? int.Parse(advertisingModel.AgeMini) : 0;
        advertisingDto.AgeMaxi = !string.IsNullOrEmpty(advertisingModel.AgeMaxi) ? int.Parse(advertisingModel.AgeMaxi) : 0;
        advertisingDto.Date = advertisingModel.Date;
        advertisingDto.StartDate = advertisingModel.StartDate;
        advertisingDto.IsPause = advertisingModel.IsPause;
        advertisingDto.Duration = !string.IsNullOrEmpty(advertisingModel.Duration) ? int.Parse(advertisingModel.Duration) : 0;
        advertisingDto.Budget = !string.IsNullOrEmpty(advertisingModel.Budget) ? int.Parse(advertisingModel.Budget) : 0;
        advertisingDto.IsPayed = advertisingModel.IsPayed;
    }


    public static void AdvertisingCopyDtoToModel(AdvertisingDtoModel advertisingDto, AdvertisingModel advertisingModel)
    {
        advertisingModel.Area ??= new AreaSelectedModel();
        advertisingModel.InterestsDetails ??= new List<InterestModel>();

        advertisingModel.AdvertisingId = advertisingDto.AdvertisingId;
        advertisingModel.Image = advertisingDto.Image;
        advertisingModel.Path = advertisingDto.Path;
        advertisingModel.Name = advertisingDto.Name;
        advertisingModel.Info = advertisingDto.Info;

        AreaCopyDtoToModel(advertisingDto.Area, advertisingModel.Area);

        advertisingModel.IdGenders = advertisingDto.IdGenders;
        advertisingModel.Gender = advertisingDto.Gender;
        advertisingModel.IsAllAge = advertisingDto.IsAllAge;
        advertisingModel.AgeMini = advertisingDto.AgeMini.ToString();
        advertisingModel.AgeMaxi = advertisingDto.AgeMaxi.ToString();
        advertisingModel.Interests = advertisingDto.Interests;

        if (advertisingDto.InterestsDetails != null)
        {
            foreach (var interestDetailsDto in advertisingDto.InterestsDetails)
            {
                var interestModel = new InterestModel();
                InterestCopyDtoToModel(interestDetailsDto, interestModel);
                advertisingModel.InterestsDetails.Add(interestModel);
            }
        }

        advertisingModel.Date = advertisingDto.Date;
        advertisingModel.StartDate = advertisingDto.StartDate;
        advertisingModel.IsPause = advertisingDto.IsPause;
        advertisingModel.Duration = advertisingDto.Duration.ToString();
        advertisingModel.Budget = advertisingDto.Budget.ToString();
        advertisingModel.IsPayed = advertisingDto.IsPayed;
        //advertisingModel.IsImage = advertisingDto.IsImage;
        advertisingModel.IsEditMode = advertisingDto.IsEditMode;
    }



    /*
     * Interest model copy
    */


    public static void InterestCopyDtoToModel(InterestDtoModel interestDto, InterestModel interestModel)
    {

        interestModel.Id = interestDto.Id;
        interestModel.Interest = interestDto.Interest;
    }


    public static void InterestCopyModelToDto(InterestModel interestModel, InterestDtoModel interestDto)
    {
        interestDto.Id = interestModel.Id;
        interestDto.Interest = interestModel.Interest;
    }



    /*
     * Area model copy
     */


    public static void AreaCopyDtoToModel(AreaSelectedDtoModel areaDto, AreaSelectedModel areaModel)
    {
        areaDto ??= new AreaSelectedDtoModel();

        if (areaModel == null)
        {
            areaModel = new AreaSelectedModel();
        }
        else
        {
            areaModel.CountryDetails = new CountryModel();
            areaModel.StatesDetails = [];
            areaModel.CountiesDetails = [];
            areaModel.CitiesDetails = [];
        }

        CountryCopyDtoToModel(areaDto.CountrySelected, areaModel.CountryDetails);

        foreach (var stateDto in areaDto.StatesSelected)
        {
            var stateModel = new StateModel();
            StateCopyDtoToModel(stateDto, stateModel);
            areaModel.StatesDetails.Add(stateModel);
        }

        foreach (var countyDto in areaDto.CountiesSelected)
        {
            var countyModel = new CountyModel();
            CountyCopyDtoToModel(countyDto, countyModel);
            areaModel.CountiesDetails.Add(countyModel);
        }

        foreach (var cityDto in areaDto.CitiesSelected)
        {
            var cityModel = new CityModel();
            CityCopyDtoToModel(cityDto, cityModel);
            areaModel.CitiesDetails.Add(cityModel);
        }

        areaModel.IsAllCountry = areaDto.IsAllCountry;
        areaModel.IsAllState = areaDto.IsAllState;
        areaModel.IsAllCounty = areaDto.IsAllCounty;
        areaModel.IsAllCity = areaDto.IsAllCity;

        areaModel.Km = areaDto.Km;
    }

    

    public static void AreaCopyModelToDto(AreaSelectedModel areaModel, AreaSelectedDtoModel areaDto)
    {
        areaModel ??= new AreaSelectedModel();

        if (areaDto == null)
        {
            areaDto = new AreaSelectedDtoModel();
        }
        else
        {
            areaDto.CountrySelected = new CountryDtoModel();
            areaDto.StatesSelected = [];
            areaDto.CountiesSelected = [];
            areaDto.CitiesSelected = [];
        }

        CountryCopyModelToDto(areaModel.CountryDetails, areaDto.CountrySelected);

        foreach (var stateModel in areaModel.StatesDetails)
        {
            var stateDto = new StateDtoModel();
            StateCopyModelToDto(stateModel, stateDto);
            areaDto.StatesSelected.Add(stateDto);
        }

        foreach (var countyModel in areaModel.CountiesDetails)
        {
            var countyDto = new CountyDtoModel();
            CountyCopyModelToDto(countyModel, countyDto);
            areaDto.CountiesSelected.Add(countyDto);
        }

        foreach (var cityModel in areaModel.CitiesDetails)
        {
            var cityDto = new CityDtoModel();
            CityCopyModelToDto(cityModel, cityDto);
            areaDto.CitiesSelected.Add(cityDto);
        }

        areaDto.IsAllCountry = areaModel.IsAllCountry;
        areaDto.IsAllState = areaModel.IsAllState;
        areaDto.IsAllCounty = areaModel.IsAllCounty;
        areaDto.IsAllCity = areaModel.IsAllCity;

        areaDto.Km = areaDto.Km;
    }


    /*
     * Country model copy
     */


    public static void CountryCopyDtoToModel(CountryDtoModel countryDto, CountryModel countryModel)
    {
        countryDto ??= new CountryDtoModel();

        countryModel.Id = countryDto.Id;
        countryModel.Country = countryDto.Country;
        countryModel.IsoAlpha2 = countryDto.IsoAlpha2;
        countryModel.PhoneCode = countryDto.PhoneCode;
    }


    public static void CountryCopyModelToDto(CountryModel countryModel, CountryDtoModel countryDto)
    {
        countryModel ??= new CountryModel();

        countryDto.Id = countryModel.Id;
        countryDto.Country = countryModel.Country;
        countryDto.IsoAlpha2 = countryModel.IsoAlpha2;
        countryDto.PhoneCode = countryModel.PhoneCode;
    }


    /*
     * State model copy
     */


    public static void StateCopyDtoToModel(StateDtoModel stateDto, StateModel stateModel)
    {
        stateDto ??= new StateDtoModel();

        stateModel.Id = stateDto.Id;
        stateModel.IdCountries = stateDto.IdCountries;
        stateModel.State = stateDto.State;
    }


    public static void StateCopyModelToDto(StateModel stateModel, StateDtoModel stateDto)
    {
        stateModel ??= new StateModel();

        stateDto.Id = stateModel.Id;
        stateDto.IdCountries = stateModel.IdCountries;
        stateDto.State = stateModel.State;
    }


    /*
     * County model copy
     */


    public static void CountyCopyDtoToModel(CountyDtoModel countyDto, CountyModel countyModel)
    {
        countyDto ??= new CountyDtoModel();

        countyModel.Id = countyDto.Id;
        countyModel.IdStates = countyDto.IdStates;
        countyModel.County = countyDto.County;
    }



    public static void CountyCopyModelToDto(CountyModel countyModel, CountyDtoModel countyDto)
    {
        countyModel ??= new CountyModel();

        countyDto.Id = countyModel.Id;
        countyDto.IdStates = countyModel.IdStates;
        countyDto.County = countyModel.County;
    }


    /*
     * City model copy
     */


    public static void CityCopyDtoToModel(CityDtoModel cityDto, CityModel cityModel)
    {
        cityDto ??= new CityDtoModel();

        cityModel.Id = cityDto.Id;
        cityModel.IdCounties = cityDto.IdCounties;
        cityModel.City = cityDto.City;
        cityModel.Lat = cityDto.Lat;
        cityModel.Lon = cityDto.Lon;
    }


    public static void CityCopyModelToDto(CityModel cityModel, CityDtoModel cityDto)
    {
        cityDto ??= new CityDtoModel();

        cityDto.Id = cityModel.Id;
        cityDto.IdCounties = cityModel.IdCounties;
        cityDto.City = cityModel.City;
        cityDto.Lat = cityModel.Lat;
        cityDto.Lon = cityModel.Lon;
    }


    /*
     *EPay Budget model copy
     */
    public static void EPayBudgetModelToDto(BudgetModel ePayBudget, BudgetDtoModel ePayBudgetDto)
    {

        ePayBudgetDto.Budget = !string.IsNullOrEmpty(ePayBudget.Budget) ? double.Parse(ePayBudget.Budget) : 1;
        ePayBudgetDto.StartDate = ePayBudget.StartDate;
        ePayBudgetDto.EndDate = ePayBudget.EndDate;
        ePayBudgetDto.Duration = !string.IsNullOrEmpty(ePayBudget.Duration) ? int.Parse(ePayBudget.Duration) : 1;
    }

    public static void EPayBudgetDtoToModel(BudgetDtoModel ePayBudgetDto, BudgetModel ePayBudget)
    {
        ePayBudget.Budget = ePayBudgetDto.Budget.ToString(CultureInfo.InvariantCulture);
        ePayBudget.StartDate = ePayBudgetDto.StartDate;
        ePayBudget.StartTime = ePayBudgetDto.StartDate.TimeOfDay;
        ePayBudget.EndDate = ePayBudgetDto.EndDate;
        ePayBudget.Duration = ePayBudgetDto.Duration.ToString();
    }

    /*
     * EPay Payment model copy
     */
    public static void EPayPaymentCopyModelToDto(EPayPaymentModel ePayPayment, EPayPaymentDtoModel ePayPaymentDto)
    {
        ePayPaymentDto.CardNo = ePayPayment.CardNo;
        ePayPaymentDto.ExpMonth = ePayPayment.ExpMonth;
        ePayPaymentDto.ExpYear = ePayPayment.ExpYear;
        ePayPaymentDto.CardCvv = ePayPayment.CardCvv;
        ePayPaymentDto.Amount = !string.IsNullOrEmpty(ePayPayment.Amount) ? int.Parse(ePayPayment.Amount) : 0;
        ePayPaymentDto.Currency = ePayPayment.Currency;
        ePayPaymentDto.Description = ePayPayment.Description;
        ePayPaymentDto.IsPayed = ePayPayment.IsPayed;
    }


    public static void EPayPaymentCopyDtoToModel(EPayPaymentDtoModel ePayPaymentDto, EPayPaymentModel ePayPayment)
    {
        ePayPayment.CardNo = ePayPaymentDto.CardNo;
        ePayPayment.ExpMonth = ePayPaymentDto.ExpMonth;
        ePayPayment.ExpYear = ePayPaymentDto.ExpYear;
        ePayPayment.CardCvv = ePayPaymentDto.CardCvv;
        ePayPayment.Amount = ePayPaymentDto.Amount.ToString();
        ePayPayment.Currency = ePayPaymentDto.Currency;
        ePayPayment.Description = ePayPaymentDto.Description;
        ePayPayment.IsPayed = ePayPaymentDto.IsPayed;
    }

    /*
     * EPay Order line model copy
     */
    public static void EPayOrderLinesCopyModelToDto(EPayOrderLinesModel ePayOrderLines, EPayOrderLinesDtoModel ePayOrderLinesDto)
    {
        ePayOrderLinesDto.IdOrders = ePayOrderLines.IdOrders;
        ePayOrderLinesDto.ProductRef = ePayOrderLines.ProductRef;
        ePayOrderLinesDto.Wording = ePayOrderLines.Wording;
        ePayOrderLinesDto.Quantity = ePayOrderLines.Quantity;
        ePayOrderLinesDto.PriceUnitHt = ePayOrderLines.PriceUnitHt;
        ePayOrderLinesDto.PriceUnitTtc = ePayOrderLines.PriceUnitTtc;
        ePayOrderLinesDto.PriceBaseHt = ePayOrderLines.PriceBaseHt;
        ePayOrderLinesDto.PriceBaseTtc = ePayOrderLines.PriceBaseTtc;
        ePayOrderLinesDto.TvaRate = ePayOrderLines.TvaRate;
        ePayOrderLinesDto.DiscountPercent = ePayOrderLines.DiscountPercent;
    }

    public static void EPayOrderLinesCopyDtoToModel(EPayOrderLinesDtoModel ePayOrderLinesDto, EPayOrderLinesModel ePayOrderLines)
    {
        ePayOrderLines.IdOrders = ePayOrderLinesDto.IdOrders;
        ePayOrderLines.ProductRef = ePayOrderLinesDto.ProductRef;
        ePayOrderLines.Wording = ePayOrderLinesDto.Wording;
        ePayOrderLines.Quantity = ePayOrderLinesDto.Quantity;
        ePayOrderLines.PriceUnitHt = ePayOrderLinesDto.PriceUnitHt;
        ePayOrderLines.PriceUnitTtc = ePayOrderLinesDto.PriceUnitTtc;
        ePayOrderLines.PriceBaseHt = ePayOrderLinesDto.PriceBaseHt;
        ePayOrderLines.PriceBaseTtc = ePayOrderLinesDto.PriceBaseTtc;
        ePayOrderLines.TvaRate = ePayOrderLinesDto.TvaRate;
        ePayOrderLines.DiscountPercent = ePayOrderLinesDto.DiscountPercent;
    }

    /*
     * EPay Address model copy
     */
    public static void EPayAddressCopyModelToDto(EPayAddressModel ePayAddress, EPayAddressDtoModel ePayAddressDto)
    {
        ePayAddressDto.Id = ePayAddress.Id;
        ePayAddressDto.Firstname = ePayAddress.Firstname;
        ePayAddressDto.Lastname = ePayAddress.Lastname;
        ePayAddressDto.PhoneNumber = ePayAddress.PhoneNumber;
        ePayAddressDto.Email = ePayAddress.Email;
        ePayAddressDto.Company = ePayAddress.Company;
        ePayAddressDto.Address1 = ePayAddress.Address1;
        ePayAddressDto.Address2 = ePayAddress.Address2;
        ePayAddressDto.Address3 = ePayAddress.Address3;
        ePayAddressDto.IdCities = ePayAddress.IdCities;
        ePayAddressDto.City = ePayAddress.City;
        ePayAddressDto.Zipcode = ePayAddress.Zipcode;
        ePayAddressDto.IdCountries = ePayAddress.IdCountries;
        ePayAddressDto.Country = ePayAddress.Country;
    }

    public static void EPayAddressCopyDtoToModel(EPayAddressDtoModel ePayAddressDto, EPayAddressModel ePayAddress)
    {
        ePayAddress.Id = ePayAddressDto.Id;
        ePayAddress.Firstname = ePayAddressDto.Firstname;
        ePayAddress.Lastname = ePayAddressDto.Lastname;
        ePayAddress.PhoneNumber = ePayAddressDto.PhoneNumber;
        ePayAddress.Email = ePayAddressDto.Email;
        ePayAddress.Company = ePayAddressDto.Company;
        ePayAddress.Address1 = ePayAddressDto.Address1;
        ePayAddress.Address2 = ePayAddressDto.Address2;
        ePayAddress.Address3 = ePayAddressDto.Address3;
        ePayAddress.IdCities = ePayAddressDto.IdCities;
        ePayAddress.City = ePayAddressDto.City;
        ePayAddress.Zipcode = ePayAddressDto.Zipcode;
        ePayAddress.IdCountries = ePayAddressDto.IdCountries;
        ePayAddress.Country = ePayAddressDto.Country;
    }


    /*
     * EPay Order model copy
     */
    public static void EPayOrderCopyModelToDto(EPayOrderModel ePayOrder, EPayOrderDtoModel ePayOrderDto)
    {
        ePayOrderDto.Id = ePayOrder.Id;
        ePayOrderDto.OrderId = ePayOrder.OrderId;
        ePayOrderDto.IdUsers = ePayOrder.IdUsers;
        ePayOrderDto.IdBillAddress = ePayOrder.IdBillAddress;
        ePayOrderDto.IdShipAddress = ePayOrder.IdShipAddress;
        ePayOrderDto.Reference = ePayOrder.Reference;
        ePayOrderDto.OrderState = ePayOrder.OrderState;
        ePayOrderDto.InvoiceNum = ePayOrder.InvoiceNum;
        ePayOrderDto.AmountTotalHt = ePayOrder.AmountTotalHt;
        ePayOrderDto.AmountTotalTtc = ePayOrder.AmountTotalTtc;
        ePayOrderDto.AmountTotalTva = ePayOrder.AmountTotalTva;
        ePayOrderDto.PortPriceTtc = ePayOrder.PortPriceTtc;
        ePayOrderDto.AmountToPay = ePayOrder.AmountToPay;
        ePayOrderDto.DateInvoice = ePayOrder.DateInvoice;
        ePayOrderDto.DateSendOrder = ePayOrder.DateSendOrder;
        ePayOrderDto.DateSendInvoice = ePayOrder.DateSendInvoice;

        foreach (var orderLine in ePayOrder.OrderLines)
        {
            var oderLineDto = new EPayOrderLinesDtoModel();
            EPayOrderLinesCopyModelToDto(orderLine, oderLineDto);
            ePayOrderDto.OrderLines.Add(oderLineDto);
        }

        EPayAddressCopyModelToDto(ePayOrder.BillingAddress, ePayOrderDto.BillingAddress);
        
        if (ePayOrder.ShippingAddress != null || !ePayOrder.IsShippingAsBilling)
            EPayAddressCopyModelToDto(ePayOrder.ShippingAddress, ePayOrderDto.ShippingAddress);

    }


    public static void EPayOrderCopyDtoToModel(EPayOrderDtoModel ePayOrderDto, EPayOrderModel ePayOrder)
    {
        ePayOrder.Id = ePayOrderDto.Id;
        ePayOrder.OrderId = ePayOrderDto.OrderId;
        ePayOrder.IdUsers = ePayOrderDto.IdUsers;
        ePayOrder.IdBillAddress = ePayOrderDto.IdBillAddress;
        ePayOrder.IdShipAddress = ePayOrderDto.IdShipAddress;
        ePayOrder.Reference = ePayOrderDto.Reference;
        ePayOrder.OrderState = ePayOrderDto.OrderState;
        ePayOrder.InvoiceNum = ePayOrderDto.InvoiceNum;
        ePayOrder.AmountTotalHt = ePayOrderDto.AmountTotalHt;
        ePayOrder.AmountTotalTtc = ePayOrderDto.AmountTotalTtc;
        ePayOrder.AmountTotalTva = ePayOrderDto.AmountTotalTva;
        ePayOrder.PortPriceTtc = ePayOrderDto.PortPriceTtc;
        ePayOrder.AmountToPay = ePayOrderDto.AmountToPay;
        ePayOrder.DateInvoice = ePayOrderDto.DateInvoice;
        ePayOrder.DateSendOrder = ePayOrderDto.DateSendOrder;
        ePayOrder.DateSendInvoice = ePayOrderDto.DateSendInvoice;

        foreach (var orderLine in ePayOrderDto.OrderLines)
        {
            var oderLineView = new EPayOrderLinesModel();
            EPayOrderLinesCopyDtoToModel(orderLine, oderLineView);
            ePayOrder.OrderLines.Add(oderLineView);
        }

        EPayAddressCopyDtoToModel(ePayOrderDto.BillingAddress, ePayOrder.BillingAddress);
        
        if (ePayOrderDto.ShippingAddress != null)
            EPayAddressCopyDtoToModel(ePayOrderDto.ShippingAddress, ePayOrder.ShippingAddress);
    }

    /*
     * EPay Details model copy
     */
    public static void EPayDetailsCopyModelToDto(EPayDetailsModel ePayDetails, EPayDetailsDtoModel ePayDetailsDto)
    {
        ePayDetailsDto.IsEPayValid = ePayDetails.IsEPayValid;

        EPayPaymentCopyModelToDto(ePayDetails.EPayPayment, ePayDetailsDto.EPayPayment);
        EPayOrderCopyModelToDto(ePayDetails.EPayOrder, ePayDetailsDto.EPayOrder);
    }


    public static void EPayDetailsCopyDtoToModel(EPayDetailsDtoModel ePayDetailsDto, EPayDetailsModel ePayDetails)
    {
        ePayDetails.IsEPayValid = ePayDetailsDto.IsEPayValid;

        EPayPaymentCopyDtoToModel(ePayDetailsDto.EPayPayment, ePayDetails.EPayPayment);
        EPayOrderCopyDtoToModel(ePayDetailsDto.EPayOrder, ePayDetails.EPayOrder);

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


    /*
     * Item model copy
     */
    public static void ItemCopyModelToDto(ItemModel itemModel, ItemDtoModel itemDto)
    {
        itemDto.Item = itemModel.Item;
        itemDto.Id = itemModel.Id;
        itemDto.IdParents = itemModel.IdParents;
    }


    public static void ItemCopyDtoToModel(ItemDtoModel itemDto, ItemModel itemModel)
    {
        itemModel.Item = itemDto.Item;
        itemModel.Id = itemDto.Id;
        itemModel.IdParents = itemDto.IdParents;
    }
}