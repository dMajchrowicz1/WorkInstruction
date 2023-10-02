using ConveyorDoc.Business.Model;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces.Services;
using ConveyorDoc.Notification;
using ConveyorDoc.ViewModels.InstructionViewModels;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using ToastNotifications;

namespace ConveyorDoc.ViewModels.ShallView
{
    public class StatusBarViewModel : BindableBase
    {
        private readonly IToastMessage _toastMessage;
      

        private string _instructionPath = string.Empty;
        public string InstructionPath
        {
            get { return _instructionPath; }
            set { SetProperty(ref _instructionPath, value); }
        }

        private int _currentlyRunningTasks;
        public int CurrentlyRunningTasks
        {
            get { return _currentlyRunningTasks; }
            set { SetProperty(ref _currentlyRunningTasks, value); }
        }

        public InstructionViewModelBase ViewModelBase { get; }

        public StatusBarViewModel(IAppTaskManager appTask, IToastMessage toastMessage, InstructionViewModelBase viewModelBase)
        {
            _toastMessage = toastMessage;
            ViewModelBase = viewModelBase;

            appTask.TaskStatusChanged  = new EventHandler<TaskFeedbackArgs>(TaskStatus_Changed);

        }


        private void TaskStatus_Changed(object sender, TaskFeedbackArgs e)
        {
            switch (e.TaskStatus)
            {
                case TaskStatus.Running:
                    CurrentlyRunningTasks = e.CurrentlyRunningTask;
                    break;
                case TaskStatus.RanToCompletion:
                    App.Current.Dispatcher.InvokeAsync(() => {
                        _toastMessage.ShowSucces($"{e.TaskTitle}. {Resources.Properties.Resources.Completed}");
                    });
                    CurrentlyRunningTasks = e.CurrentlyRunningTask;
                    break;
                case TaskStatus.Canceled:
                    App.Current.Dispatcher.InvokeAsync(() => { _toastMessage.ShowWarning(e.TaskError); });
                    CurrentlyRunningTasks = e.CurrentlyRunningTask;
                    break;
                case TaskStatus.Faulted:
                    App.Current.Dispatcher.InvokeAsync(() => { _toastMessage.ShowError(e.TaskError); });
                    CurrentlyRunningTasks = e.CurrentlyRunningTask;
                    break;
            }


        }


    }
}
