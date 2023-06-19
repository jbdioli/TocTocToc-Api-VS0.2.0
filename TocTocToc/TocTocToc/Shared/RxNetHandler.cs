using System.Collections.Generic;
using System.Reactive.Subjects;
using TocTocToc.Models.Dto;

namespace TocTocToc.Shared;

public static class RxNetHandler
{
    public static readonly ReplaySubject<UserDtoModel> UserSubject = new(1);
    public static readonly ReplaySubject<List<AddressDtoModel>> AddressesSubject = new(1);
    public static readonly ReplaySubject<List<AdvertisingDtoModel>> AdsSubject = new(1);
    public static readonly ReplaySubject<List<AdvertisingDtoModel>> AdvertisementsSubject = new(1);
    public static readonly ReplaySubject<AdvertisingDtoModel> AdvertisingSubject = new(1);
    public static readonly ReplaySubject<SettingDtoModel> SettingSubject = new(1);

}