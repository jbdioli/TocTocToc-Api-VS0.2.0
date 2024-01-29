using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TocTocToc.Resx;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TocTocToc.Models.View
{
    public class LanguageViewModel : BaseViewModel
    {
        List<(Func<string> name, string value)> LanguageMapping { get; } = new()
        {
            (() => AppResources.System, null),
            (() => AppResources.English, "en"),
            (() => AppResources.French, "fr"),
        };

        public LocalizedString CurrentLanguage { get; }

        public LocalizedString Version { get; } = new(() => string.Format(AppResources.Version, AppInfo.VersionString));

        public ICommand ChangeLanguageCommand { get; }

        public LanguageViewModel()
        {
            CurrentLanguage = new LocalizedString(GetCurrentLanguageName);

            ChangeLanguageCommand = new AsyncCommand(ChangeLanguage);
        }

        private string GetCurrentLanguageName()
        {
            var (knownName, _) = LanguageMapping.SingleOrDefault(m => m.value == LocalizationResourceManager.Current.CurrentCulture.TwoLetterISOLanguageName);
            return knownName != null ? knownName() : LocalizationResourceManager.Current.CurrentCulture.DisplayName;
        }

        public async Task<string> ChangeLanguage()
        {
            var selectedName = await Application.Current.MainPage.DisplayActionSheet(
                AppResources.ChangeLanguage,
                null, null,
                LanguageMapping.Select(m => m.name()).ToArray());
            if (selectedName == null)
            {
                return string.Empty;
            }

            var selectedValue = LanguageMapping.Single(m => m.name() == selectedName).value;
            LocalizationResourceManager.Current.CurrentCulture = selectedValue == null ? CultureInfo.CurrentCulture : new CultureInfo(selectedValue);
            return selectedValue;
        }


        public static void SetLanguage(string isoLanguage)
        {
            LocalizationResourceManager.Current.CurrentCulture = string.IsNullOrWhiteSpace(isoLanguage) ? CultureInfo.CurrentCulture : new CultureInfo(isoLanguage);
        }

    }
}