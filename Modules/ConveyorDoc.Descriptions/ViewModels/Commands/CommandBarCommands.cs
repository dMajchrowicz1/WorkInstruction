using ConveyorDoc.Core;
using ConveyorDoc.Core.Extension;
using ConveyorDoc.Descriptions.Interfaces;
using ConveyorDoc.Descriptions.Model;
using ConveyorDoc.Descriptions.Views.Dialogs;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace ConveyorDoc.Descriptions.ViewModels.Commands
{
    public class CommandBarCommands
    {
        private readonly IAppTask _appTask;

        private readonly IDescriptionRepository _descriptionRepository;

        private readonly IWindowsDialogService _windowsDialogService;

        private readonly DescriptionViewModelBase _viewModelBase;


        private DelegateCommand<DescriptionRecord> _addDescriptionCommand;
        public DelegateCommand<DescriptionRecord> AddDescriptionCommand =>
            _addDescriptionCommand ?? (_addDescriptionCommand = new DelegateCommand<DescriptionRecord>(ExecuteAddDescriptionCommand));

        private DelegateCommand<DescriptionRecord> _updateDescriptionCcommand;
        public DelegateCommand<DescriptionRecord> UpdateDescriptionCommand =>
            _updateDescriptionCcommand ?? (_updateDescriptionCcommand = new DelegateCommand<DescriptionRecord>(ExecuteUpdateDescriptionCommand));

        private DelegateCommand<DescriptionRecord> _deleteDescriptionCommand;
        public DelegateCommand<DescriptionRecord> DeleteDescriptionCommand =>
            _deleteDescriptionCommand ?? (_deleteDescriptionCommand = new DelegateCommand<DescriptionRecord>(ExecuteDeleteDescriptionCommand));

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshListCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshListCommand));



        public CommandBarCommands(IAppTask appTask, 
            IDescriptionRepository descriptionRepository, 
            IWindowsDialogService windowsDialogService,
            DescriptionViewModelBase viewModelBase)
        {
            _descriptionRepository = descriptionRepository; 

            _appTask = appTask;

            _viewModelBase = viewModelBase;

            _windowsDialogService = windowsDialogService;
        }


        private void ExecuteRefreshListCommand()
        {

            _appTask.RunAsync(() =>
            {
                return _descriptionRepository.GetAll();

            }, Resources.Properties.Resources.ReloadDatabase,(status,data) =>
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    _viewModelBase.Descriptions.Replace(data as IEnumerable<DescriptionRecord>);
                    _viewModelBase.DescriptionCollection.Refresh();
                });
            });

        }

        private void ExecuteDeleteDescriptionCommand(DescriptionRecord parameter)
        {
            _windowsDialogService.ShowContentDialog(typeof(RemoveDescriptionDoubleCheckDialog), callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    _appTask.RunAsync(() =>
                    {
                        _descriptionRepository.Delete(parameter);
                    }
                    , ConveyorDoc.Resources.Properties.Resources.DeletingRecord, result =>
                    {
                      

                        if (result == TaskStatus.RanToCompletion)
                        {
                            Application.Current?.Dispatcher.InvokeAsync(() =>
                            {
                                _viewModelBase.Descriptions.Remove(parameter);
                            });                            
                        }
                    });
                }

            });


        }

        private void ExecuteUpdateDescriptionCommand(DescriptionRecord parameter)
        {
            _appTask.RunAsync(() =>
            {
                _descriptionRepository.Update(parameter);

            }, Resources.Properties.Resources.UpdatingItem);
        }

        private async void ExecuteAddDescriptionCommand(DescriptionRecord parameter)
        {
            IDialogResult dialogResult = null;

            _windowsDialogService.ShowContentDialog(typeof(AddDescriptionDialog), result =>
            {
                dialogResult = result;

                if (dialogResult != null && dialogResult.Result == ButtonResult.OK)
                {

                    DescriptionRecord item = dialogResult.Parameters.GetValue<DescriptionRecord>("entity");

                    _appTask.RunAsync(() =>
                    {
                        _descriptionRepository.Insert(item);

                    }, Resources.Properties.Resources.AddingItem, callback =>
                    {
                        if (callback == TaskStatus.RanToCompletion)
                        {

                            Application.Current.Dispatcher.Invoke(() => 
                            { _viewModelBase.Descriptions.Add(item); });
                            
                        }

                    });
                }
            });
        }
    }
}
