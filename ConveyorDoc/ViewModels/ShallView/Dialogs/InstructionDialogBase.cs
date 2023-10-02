using ConveyorDoc.Business.Model;
using ConveyorDoc.Core;
using ConveyorDoc.Descriptions.Interfaces;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Model;
using ConveyorDoc.Model.Settings;
using ConveyorDoc.Notification;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;

namespace ConveyorDoc.ViewModels.ShallView.Dialogs
{
    public class InstructionDialogBase : BindableBase , IDialogAware2
    {
        private string _title;

        private string _primaryButtonText;

        private Notifier _notifier;

        private Instruction _dialogInstruction;
        public Instruction DialogInstruction
        {
            get { return _dialogInstruction; }
            set { SetProperty(ref _dialogInstruction, value); }
        }

        public string Title => _title;

        public string PrimaryButtonText => _primaryButtonText;

        public event Action<IDialogResult> RequestClose;

        public IDataContainer Data { get; }



        public InstructionDialogBase(
            string title, string primaryButtonText, Notifier notifier, 
            IDataContainer data) 
        {
            _title = title;

            _primaryButtonText = primaryButtonText;

            _notifier = notifier;

            _dialogInstruction = new Instruction();

            Data = data;

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters != null && parameters.ContainsKey("instruction"))
            {
                DialogInstruction = parameters.GetValue<Instruction>("instruction");
            }
        }

        public void OnPrimaryButtonPressed()
        {
            if (DialogInstruction.HasErrors)
            {
                _notifier.ShowWarning(Resources.Properties.Resources.ErrorInstructionValidation);
            }
            else
            {
                var dialogParameter = new DialogParameters();
                dialogParameter.Add("dialogInstruction", DialogInstruction);

                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dialogParameter));
            }
        }
    }
}
