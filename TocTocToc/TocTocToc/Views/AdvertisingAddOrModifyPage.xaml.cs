using System;
using System.Collections.Generic;
using TocTocToc.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;

namespace TocTocToc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisingAddOrModifyPage : ContentPage
    {

        public AdvertisingAddOrModifyPage()
        {
            InitializeComponent();

        }


        protected override async void OnAppearing()
        {

        }


        protected override void OnDisappearing()
        {
            RxNetHandler.AdvertisingSubject.Clear();
        }

    }
}