using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using TocTocToc.Services;
using TocTocToc.Shared;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TocTocToc.Models.View;

public class AdvertisingHistoryViewModel : BaseViewModel
{
    private readonly NotificationChannelHandler _notificationChannelHandler = new(new DisplayNotification());
    private readonly HttpRequestChannelHandler _httpRequestChannelHandler = new(new AdvertisingStorageServiceChannel());

    public ObservableCollection<AdvertisingModel> ObserverAdvertisingViewModels { get; set; } = new ();

    private List<AdvertisingModel> _advertisingModel = new();
    private List<AdvertisingDtoModel> _advertisementsDto = new();

    private IDisposable _disposed = null;

    private ICommand Init { get; }
    private ICommand ClearRxNet { get; }
    public AsyncCommand<AdvertisingModel> PausedAsyncCommand { get; }
    public AsyncCommand<AdvertisingModel> EditedAsyncCommand { get; }
    public AsyncCommand<AdvertisingModel> DeletedAsyncCommand { get; }


    public AdvertisingHistoryViewModel()
    {
        Init = new AsyncCommand(InitAsync);
        Init.Execute(null);
        ClearRxNet = new AsyncCommand(ClearRxNetAsync);

        PausedAsyncCommand = new AsyncCommand<AdvertisingModel>(PauseAsync);
        EditedAsyncCommand = new AsyncCommand<AdvertisingModel>(EditAsync);
        DeletedAsyncCommand = new AsyncCommand<AdvertisingModel>(DeleteAsync);

        IsBusy = true;
    }


    private async Task InitAsync()
    {
        await GetAdvertising();
        if (_disposed != null) return;
        SubscribeToData();
    }

    private Task ClearRxNetAsync()
    {
        _disposed?.Dispose();
        _disposed = null;
        return Task.CompletedTask;
    }

    private async Task GetAdvertising()
    {
        await _httpRequestChannelHandler.GetHttpAsync<List<AdvertisingDtoModel>>();
    }


    private void SubscribeToData()
    {
        _disposed = RxNetHandler.AdvertisementsSubject.Subscribe(
            advertisements =>
            {
                if (_advertisementsDto.Count > 0)
                {
                    _advertisementsDto.Clear();
                    _advertisingModel.Clear();
                    ObserverAdvertisingViewModels.Clear();
                }

                _advertisementsDto = advertisements;
                CopyAdvertisingToCollection();
                IsBusy = false;
            },
            () =>
            {
                Console.WriteLine("[ completed ]");
            }
        );

    }


    private void CopyAdvertisingToCollection()
    {
        _advertisingModel = new List<AdvertisingModel>();

        foreach (var advertising in _advertisementsDto)
        {
            var advertisingModel = new AdvertisingModel();
            CopyModel.AdvertisingCopyDtoToModel(advertising, advertisingModel);
            advertisingModel.FullPathImage = WebConstants.Url + advertising.Path + advertising.Image;
            _advertisingModel.Add(advertisingModel);
            ObserverAdvertisingViewModels.Add(advertisingModel);
        }
    }


    private async Task DeleteAsync(AdvertisingModel advertisingModel)
    {
        if (advertisingModel == null) return;
        var advertisementId = advertisingModel.AdvertisingId;
        var index = _advertisingModel.FindIndex(el => el.AdvertisingId.Equals(advertisementId));
        _advertisingModel.RemoveAt(index);
        ObserverAdvertisingViewModels.RemoveAt(index);
        await _httpRequestChannelHandler.DeleteHttpAsync<List<AdvertisingDtoModel>>(advertisementId);
    }

    private async Task PauseAsync(AdvertisingModel advertisingModel)
    {
        if ( advertisingModel == null) return;
        if (advertisingModel.IsPayed == false)
        {
            _notificationChannelHandler.SendNotification(ENotificationType.IsPayedNeed, null);
            return;
        }

        advertisingModel.IsPayed = !advertisingModel.IsPayed;

        var advertisingDto = new AdvertisingDtoModel();
        CopyModel.AdvertisingCopyModelToDto(advertisingModel, advertisingDto);

        await _httpRequestChannelHandler.UpdateHttpAsync<AdvertisingDtoModel, AdvertisingDtoModel>(advertisingDto);
    }


    private async Task EditAsync(AdvertisingModel advertisingModel)
    {
        if (advertisingModel == null) return;

        var advertising = new AdvertisingDtoModel();

        CopyModel.AdvertisingCopyModelToDto(advertisingModel, advertising);
        advertising.IsEditMode = true;
        if (!string.IsNullOrWhiteSpace(advertisingModel.Image))
            advertising.IsImage = true;
        RxNetHandler.AdvertisingSubject.OnNext(advertising);
        await Application.Current.MainPage.Navigation.PopAsync();
        ClearRxNet.Execute(null);
    }

}