using ConveyorDoc.Business.Extension;
using ConveyorDoc.Business.Model;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Notification;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ToastNotifications;

namespace ConveyorDoc.ViewModels.InstructionViewModels.Dialogs
{
    public class SelectModuleProgramDialogViewModel : BindableBase, IDialogAware2
    {

        public string PrimaryButtonText => Resources.Properties.Resources.Add;

        public string Title => ConveyorDoc.Resources.Properties.Resources.OpenFileNcProgram;

        public event Action<IDialogResult> RequestClose;


        private ObservableCollection<NcProgram> _modulePrograms;
        public ObservableCollection<NcProgram> ModulePrograms
        {
            get { return _modulePrograms; }
            set { SetProperty(ref _modulePrograms, value); }
        }


        public SelectModuleProgramDialogViewModel(IGetModuleProgramsQuery query,InstructionViewModelBase viewModelBase) 
        {            
            _modulePrograms = new ObservableCollection<NcProgram>(query.GetModulePrograms(viewModelBase.CurrentInstruction.ModuleNumber));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public void OnPrimaryButtonPressed()
        {
            var parameter = new DialogParameters();
            parameter.Add("programs", ModulePrograms.Where(x=>x.IsSelected == true));


            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameter));
        }


    }
}
