using System.Collections.Generic;
using System.Reactive.Subjects;
using TocTocToc.Models.Dto;

namespace TocTocToc.Shared;

public static class RxNetHandler
{
    public static readonly RxNetReplaySubject<UserDtoModel> UserSubject = new();
    public static readonly RxNetReplaySubject<List<AddressDtoModel>> AddressesSubject = new();
    public static readonly RxNetReplaySubject<List<AdvertisingDtoModel>> AdsSubject = new();
    public static readonly RxNetReplaySubject<List<AdvertisingDtoModel>> AdvertisementsSubject = new();
    public static readonly RxNetReplaySubject<AdvertisingDtoModel> AdvertisingSubject = new();
    public static readonly RxNetReplaySubject<SettingDtoModel> SettingSubject = new();




    //public static readonly ReplaySubject<UserDtoModel> UserSubject = new(1);
    //public static readonly ReplaySubject<List<AddressDtoModel>> AddressesSubject = new(1);
    //public static readonly ReplaySubject<List<AdvertisingDtoModel>> AdsSubject = new(1);
    //public static readonly ReplaySubject<List<AdvertisingDtoModel>> AdvertisementsSubject = new(1);
    //public static readonly ReplaySubject<AdvertisingDtoModel> AdvertisingSubject = new(1);
    //public static readonly ReplaySubject<SettingDtoModel> SettingSubject = new(1);


}