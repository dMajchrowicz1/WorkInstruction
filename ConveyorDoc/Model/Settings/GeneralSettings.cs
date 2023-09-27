using ConveyorDoc.Business.Constants;
using ModernWpf;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Windows.System.UserProfile;


namespace ConveyorDoc.Model.Settings
{
    public class GeneralSettings : BindableBase
    {
        private int _windowsWidth = 1200;
        public int WindowsWidth
        {
            get { return _windowsWidth; }
            set { SetProperty(ref _windowsWidth, value); }
        }

        private int _windowsHeight = 800;
        public int WindowsHeight
        {
            get { return _windowsHeight; }
            set { SetProperty(ref _windowsHeight, value); }
        }

        private Color _appColor = Color.FromRgb(0, 102, 255);
        public Color AppColor
        {
            get { return _appColor; }
            set { SetProperty(ref _appColor, value); }
        }

        private ApplicationTheme _themeName = ApplicationTheme.Dark;
        public ApplicationTheme ThemeName
        {
            get { return _themeName; }
            set { SetProperty(ref _themeName, value); }
        }

        private string _language = "en-UK";
        public string Language
        {
            get { return _language; }
            set { SetProperty(ref _language, value); }
        }

        private List<CultureInfo> _languages = new List<CultureInfo>();
        [JsonIgnore]
        public List<CultureInfo> Languages
        {
            get { return _languages; }
            set { _languages = value; }
        }

        public GeneralSettings()
        {
            Languages.Add(new CultureInfo("en-UK"));
            Languages.Add(new CultureInfo("pl-PL"));

            foreach (var language in Languages)
            {
                language.DateTimeFormat.ShortDatePattern = GeneralConstants.DATE_TIME_FORMAT;
            }
        }

        public void InitSettings()
        {
            ChangeTheme();
            ChangeWindowSize();
            ChangeLanguage();
        }

        public void ChangeTheme()
        {
            ThemeManager.Current.ApplicationTheme = ThemeName;
        }

        public void ChangeWindowSize()
        {
            if (WindowsHeight >= 800 && WindowsWidth >= 1200)
            {
                Application.Current.MainWindow.Width = WindowsWidth;
                Application.Current.MainWindow.Height = WindowsHeight;
            }
            else
                throw new Exception("Min windows width: 1200 and height: 800. Cannot change");

        }

        public void ChangeLanguage()
        {
            CultureInfo culture = _languages.FirstOrDefault(x => x.IetfLanguageTag == Language);


            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
