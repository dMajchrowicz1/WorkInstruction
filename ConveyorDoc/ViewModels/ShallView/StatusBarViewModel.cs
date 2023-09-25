using ConveyorDoc.Core;
using ConveyorDoc.Interfaces.Services;
using ConveyorDoc.Notification;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using ToastNotifications;

namespace ConveyorDoc.ViewModels.ShallView
{
    public class StatusBarViewModel : BindableBase
    {
        private readonly IToastMessage _toastMessage;

        private readonly IActiveTasks _tasks;
        

        private string _instructionPath;
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

        public StatusBarViewModel(IAppTaskManager appTask,IActiveTasks tasks, IToastMessage toastMessage)
        {
            _toastMessage = toastMessage;
            _tasks = tasks;

            appTask.TaskStatusChanged = new EventHandler<TaskFeedbackArgs>(TaskCompleted_Changed);
        }

        private void TaskCompleted_Changed(object sender, TaskFeedbackArgs e)
        {

            

            if (sender is AppTaskManager manager)
            {
                switch (e.TaskStatus)
                {
                    case TaskStatus.Created:
                        break;
                    case TaskStatus.RanToCompletion:
                        //_notifier.ShowSucces($"{e.TaskTitle}. {Resources.Properties.Resources.Completed}");
                        break;
                    case TaskStatus.Canceled:
                        App.Current.Dispatcher.InvokeAsync(() =>
                        {
                            _toastMessage.ShowWarning(e.TaskError);
                        });
                        break;
                    case TaskStatus.Faulted:
                        App.Current.Dispatcher.InvokeAsync(() => { _toastMessage.ShowError(e.TaskError); });
                        break;
                }
            }


            CurrentlyRunningTasks = _tasks.ActiveTasks.Count;

        }


    }
}
