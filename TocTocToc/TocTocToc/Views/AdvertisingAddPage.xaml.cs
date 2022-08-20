using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TocTocToc.DtoModels;
using TocTocToc.Popup;
using TocTocToc.Services;
using TocTocToc.Shared;
using TocTocToc.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisingAddPage : ContentPage
    {
        private readonly AdvertisingStorageService _advertisingStorageService;
        private AdvertisingViewModel _advertisingViewModel = new();
        private readonly ItemsStorageService _itemsStorageService = new();

        private CopyModel _copyModel = new();

        private IDisposable _disposed = null;

        private string _userId;
        private List<AdvertisingDto> _advertisingDto = new();


        public AdvertisingAddPage()
        {
            InitializeComponent();

            _userId = LocalStorageService.GetUserId();

            _advertisingStorageService = new AdvertisingStorageService(_userId);


        }


        protected override async void OnAppearing()
        {
            if (_disposed != null) return;
            
            _advertisingViewModel = await _itemsStorageService.SetGenders(_advertisingViewModel);
            SetupPicker();
            BindingContext = _advertisingViewModel;

            SubscribeToData();
        }


        private async void OnAddMedia(object sender, EventArgs e)
        {

            var file = await FilePicker.PickAsync(new PickOptions()
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an Image"
            });

            if (file == null) return;

            var fileName = file.FileName;
            var path = file.FullPath;
            var stream = await file.OpenReadAsync();
            var image = ImageSource.FromStream(() => stream);
            _advertisingViewModel.Image = path;
            _advertisingViewModel.FullPathImage = path;
            //await _advertisingStorageService.PostMedia(_userId, _advertisingViewModel.Image);
            BindingContext = _advertisingViewModel;
        }

        private void OnValidated(object sender, EventArgs e)
        {

        }

        private void OnGender(object sender, EventArgs e)
        {
            var picker = (Xamarin.Forms.Picker)sender;
            var genderDetails = (GenderDto)picker.SelectedItem;
            _advertisingViewModel.IdGenders = genderDetails.Id;
        }

        private async void OnHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdvertisingHistoryPage());
        }


        private async void OnMediaDetails(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new AdvertisingDisplayPopup(_advertisingViewModel));
        }


        private void SetupPicker()
        {
            XNameGenderPicker.ItemsSource = (System.Collections.IList)_advertisingViewModel.Genders;
        }


        private void SubscribeToData()
        {
            _disposed = _advertisingStorageService.AdvertisingSubject.Subscribe(
                advertising =>
                {
                    if (advertising == null) return;

                    _advertisingViewModel = new AdvertisingViewModel();
                    BindingContext = _advertisingViewModel;

                    _copyModel.AdvertisingCopyDtoToViewModel(advertising, _advertisingViewModel);
                    SetupPicker();
                    BindingContext = _advertisingViewModel;

                },
                () =>
                {
                    Console.WriteLine("[ completed ]");
                }
            );
        }


        protected override void OnDisappearing()
        {
            _disposed.Dispose();
        }


    }
}