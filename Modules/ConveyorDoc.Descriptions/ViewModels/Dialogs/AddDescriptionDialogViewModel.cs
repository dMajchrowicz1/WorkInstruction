using ConveyorDoc.Core;
using ConveyorDoc.Descriptions.Interfaces;
using ConveyorDoc.Descriptions.Model;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConveyorDoc.Descriptions.ViewModels.Dialogs
{
    public class AddDescriptionDialogViewModel : BindableBase, IDialogAware2
    {

        private DescriptionRecord _description = new DescriptionRecord();
        public DescriptionRecord Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Title => $"{Resources.Properties.Resources.Add} {Resources.Properties.Resources.Description}";

        public string PrimaryButtonText => $"{Resources.Properties.Resources.Add}";

        public event Action<IDialogResult> RequestClose;

        public IModuleTypesContainer Modules { get; }

        public DescriptionViewModelBase ViewModelBase { get; }


        public AddDescriptionDialogViewModel(DescriptionViewModelBase viewModelBase, IModuleTypesContainer modules)
        {
            
            ViewModelBase = viewModelBase;

            Modules = modules;

        }


        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public void OnPrimaryButtonPressed()
        {
            var parameter = new DialogParameters();
            parameter.Add("entity", Description);

            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameter));
        }
    }
}
