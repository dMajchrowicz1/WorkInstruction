using ConveyorDoc.Fixtures.Interfaces;
using Prism.Commands;
using System;
using ConveyorDoc.Core;
using Prism.Services.Dialogs;
using ConveyorDoc.Fixtures.Model;
using System.Collections.Generic;
using ConveyorDoc.Core.Extension;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows;
using ConveyorDoc.Fixtures.Views.Dialogs;

namespace ConveyorDoc.Fixtures.ViewModels
{
    public class FixtureCommandBarViewModel : BindableBase
    {
        private readonly IFixtureRepository _fixtureRepository;

        private readonly IAppTask _appTask;

        private readonly IWindowsDialogService _dialogService;


        //Commands
        private DelegateCommand _AddNewItemCommand;
        public DelegateCommand AddNewItemCommand =>
            _AddNewItemCommand ?? (_AddNewItemCommand = new DelegateCommand(ExecuteAddFixtureCommand));


        private DelegateCommand<FixtureRecord> _delecteItemCommand;
        public DelegateCommand<FixtureRecord> DeleteItemCommand =>
            _delecteItemCommand ?? (_delecteItemCommand = new DelegateCommand<FixtureRecord>(ExecuteDeleteFixtureCommand));


        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshListCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshFxituresCommand));


        private DelegateCommand<object> _openWorkspaceCommand;
        public DelegateCommand<object> OpenWorkspaceCommand =>
            _openWorkspaceCommand ?? (_openWorkspaceCommand = new DelegateCommand<object>(ExecuteOpenWorkspaceCommand));


        private DelegateCommand<object> _openDrawingCommand;
        public DelegateCommand<object> OpenDrawingCommand =>
            _openDrawingCommand ?? (_openDrawingCommand = new DelegateCommand<object>(ExecuteOpenDrawingCommand));

        private DelegateCommand<FixtureRecord> _editFixtureCommand;
        public DelegateCommand<FixtureRecord> EditFixtureCommand =>
            _editFixtureCommand ?? (_editFixtureCommand = new DelegateCommand<FixtureRecord>(ExecuteEditFixtureCommand));


        private FixtureViewModelBase _viewModelBase;
        public FixtureViewModelBase ViewModelBase
        {
            get { return _viewModelBase; }
            set { SetProperty(ref _viewModelBase, value); }
        }

      

        public FixtureCommandBarViewModel(
            IFixtureRepository fixtureRepository, 
            IAppTask appTask, 
            IWindowsDialogService dialogService,
            FixtureViewModelBase viewModelBase)
        {
            _fixtureRepository = fixtureRepository;
            _appTask = appTask;
            _dialogService = dialogService;
            _viewModelBase = viewModelBase;
        }

        private void ExecuteAddFixtureCommand()
        {
            IDialogResult dialogResult = null;

            _dialogService.ShowContentDialog(typeof(AddFixtureDialog), result =>
            {
                dialogResult = result;

                if (dialogResult != null && dialogResult.Result == ButtonResult.OK)
                {

                    FixtureRecord item = dialogResult.Parameters.GetValue<FixtureRecord>("entity");

                    _appTask.Run(() =>
                    {
                        _fixtureRepository.Insert(item);

                    }, Resources.Properties.Resources.AddingItem, callback =>
                    {
                        if (callback == TaskStatus.RanToCompletion)
                        {
                            ViewModelBase.Fixtures.Add(item);
                        }

                    });
                }
            });


        }

        private void ExecuteDeleteFixtureCommand(FixtureRecord obj)
        {
            _dialogService.ShowContentDialog(typeof(ConveyorDoc.Resources.Reusable.RemoveDoubleCheckDialog), result =>
            {
                if(result.Result == ButtonResult.OK)
                {
                    _appTask.Run(() => { _fixtureRepository.Delete(obj); }
                    , ConveyorDoc.Resources.Properties.Resources.DeletingRecord, result =>
                    {
                        if (result == TaskStatus.RanToCompletion)
                        {
                            ViewModelBase.Fixtures.Remove(obj);
                        }
                    });
                }

            });

        }

        private void ExecuteRefreshFxituresCommand()
        {

            _appTask.Run(() => 
            { 
                return _fixtureRepository.GetAll(); 
            }
            , Resources.Properties.Resources.ReloadDatabase, (status, data) =>
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ViewModelBase.Fixtures.Replace(data as IEnumerable<FixtureRecord>);
                    ViewModelBase.FixtureCollection.Refresh();
                });
            });
        }

        private void ExecuteOpenWorkspaceCommand(object obj)
        {
            if (obj is FixtureRecord fix)
            {
                fix.OpenWorkspace();
            }
        }

        private void ExecuteOpenDrawingCommand(object parameter)
        {
            if (parameter is FixtureRecord record)
            {
                record.OpenPdf();
            }
        }

        private void ExecuteEditFixtureCommand(FixtureRecord obj)
        {
            obj.ModifiedBy = Environment.UserName;
            obj.ModificationDate = DateTime.Now.ToString("MM.dd.yyyy HH:mm:ss");


            _appTask.Run(() =>
            {
                _fixtureRepository.Update(obj);

            }, Resources.Properties.Resources.UpdatingItem);

        }
    }
}
