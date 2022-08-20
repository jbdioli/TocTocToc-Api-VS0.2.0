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

namespace TocTocToc.ViewModels
{
    public class LanguageViewModel : ObservableObject
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
            CurrentLanguage = new(GetCurrentLanguageName);

            ChangeLanguageCommand = new AsyncCommand(ChangeLanguage);
        }

        private string GetCurrentLanguageName()
        {
            var (knownName, _) = LanguageMapping.SingleOrDefault(m => m.value == LocalizationResourceManager.Current.CurrentCulture.TwoLetterISOLanguageName);
            return knownName != null ? knownName() : LocalizationResourceManager.Current.CurrentCulture.DisplayName;
        }

        async Task ChangeLanguage()
        {
            var selectedName = await Application.Current.MainPage.DisplayActionSheet(
                AppResources.ChangeLanguage,
                null, null,
                LanguageMapping.Select(m => m.name()).ToArray());
            if (selectedName == null)
            {
                return;
            }

            var selectedValue = LanguageMapping.Single(m => m.name() == selectedName).value;
            LocalizationResourceManager.Current.CurrentCulture = selectedValue == null ? CultureInfo.CurrentCulture : new CultureInfo(selectedValue);
        }

    }
}