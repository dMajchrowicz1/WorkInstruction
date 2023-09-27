using ConveyorDoc.Business.Constants;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Model.Settings;
using ConveyorDoc.Notification;
using ModernWpf;
using Prism.Commands;
using Prism.Mvvm;
using System.Globalization;
using ToastNotifications;

namespace ConveyorDoc.Settings.ViewModels
{
    public class SettingsViewModel : BindableBase
    {

        public IApplicationCommands ApplicationCommands { get; }

        public AppSettings Settings { get; private set; }


        public DelegateCommand SaveSettingsCommand { get; private set; }
        public DelegateCommand ChangeWindowSizeCommand { get; private set; }
        public DelegateCommand ChangeLanguageCommand { get; private set; }
        public DelegateCommand LoadSettingsCommand { get; private set; }
        public DelegateCommand OnThemeChanged { get; private set; }

              

        public SettingsViewModel(AppSettings appSettings, 
            IWindowsDialogService windowsDialogService, 
            IApplicationCommands appCommands,
            IAppTask appTask,
            Notifier notifier)
        {

            Settings = appSettings;

            ApplicationCommands = appCommands;  

            SaveSettingsCommand = new DelegateCommand(() =>
            {
                windowsDialogService.ShowFolderBrowseDialog(callback =>
                {
                    if (callback.Result == Prism.Services.Dialogs.ButtonResult.OK)
                    {
                        Settings.Save(callback.Parameters.GetValue<string>("parameter"));
                    }

                }, GeneralConstants.CONFIG_FILE_DIR);
                
            });

            LoadSettingsCommand = new DelegateCommand(() =>
            {
                   windowsDialogService.ShowOpenFileDialog(Resources.Properties.Resources.SelectConfigurationFile
                    , GeneralConstants.CONFIG_DIALOG_FILTER,callback=>
                    {
                        Settings = AppSettings.Load(callback.Parameters.GetValue<string>("parameter"));

                    },GeneralConstants.CONFIG_FILE_DIR, false);

            });

            ChangeWindowSizeCommand = new DelegateCommand(() =>
            {
                appTask.RunOnUIThread(() =>
                {
                    Settings.GeneralSettings.ChangeWindowSize();
                }, ConveyorDoc.Resources.Properties.Resources.ChangingWindowSize);
                
            });

            ChangeLanguageCommand = new DelegateCommand(() =>
            {
                notifier.ShowInformation(Resources.Properties.Resources.LanguageChangeInfo);
            });

            OnThemeChanged = new DelegateCommand(() =>
            {
                Settings.GeneralSettings.ThemeName = ThemeManager.Current.ApplicationTheme.Value;
            });

        }

    }
}
