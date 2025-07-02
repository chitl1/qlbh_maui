using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace qlbb2.Helper
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        public static LocalizationResourceManager Instance { get; } = new LocalizationResourceManager();

        public string this[string text]
            => Resources.Localization.AppResources.ResourceManager.GetString(text, Resources.Localization.AppResources.Culture) ?? text;

        public void SetCulture(CultureInfo culture)
        {
            Resources.Localization.AppResources.Culture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
