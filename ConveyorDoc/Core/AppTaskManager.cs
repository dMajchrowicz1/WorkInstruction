

using ConveyorDoc.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace ConveyorDoc.Core
{

    public class AppTaskManager : IAppTask, IAppTaskManager
    {

        private List<string> _activeTasks { get; } = new List<string>();

        public EventHandler<TaskFeedbackArgs> TaskStatusChanged { get; set; }



        public void RunAsync(Action action, string taskTitle, Action<TaskStatus> onCompleted = null)
        {
            Task task = null;


            if (_activeTasks.Contains(taskTitle))
            {
                TaskStatusChanged?.Invoke
                    (this, new TaskFeedbackArgs(taskTitle, Resources.Properties.Resources.AppTaskErrorMultipleTask,
                    TaskStatus.Canceled, _activeTasks.Count));
            }
            else
            {
                _activeTasks.Add(taskTitle);
                task = new Task(action);
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.Running, _activeTasks.Count));

                task.Start();
                

                //Completed
                task.Await(() =>
                {
                    _activeTasks.Remove(taskTitle);

                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle,
                        string.Empty, task.Status, _activeTasks.Count));


                    onCompleted?.Invoke(task.Status);
                   

                    task.Dispose();
                }, error =>
                {
                    _activeTasks.Remove(taskTitle);

                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle,
                            error.Message, TaskStatus.Faulted, _activeTasks.Count));

                    onCompleted?.Invoke(TaskStatus.Faulted);

                    task.Dispose();
                });

            }
        }

        public  void RunAsync(Func<object> action, string taskTitle, Action<TaskStatus, object> onCompleted = null)
        {

            Task<object> task = null;



            if (_activeTasks.Contains(taskTitle))
            {
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle,
                    Resources.Properties.Resources.AppTaskErrorMultipleTask,
                    TaskStatus.Canceled, _activeTasks.Count));
            }
            else
            {
                _activeTasks.Add(taskTitle);
                task = new Task<object>(action);
                task.Start();
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.Running, _activeTasks.Count));

                

                //Completed
                task.Await(() =>
                {
                    _activeTasks.Remove(taskTitle);

                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle,
                        string.Empty, task.Status, _activeTasks.Count));


                    onCompleted?.Invoke(task.Status,task.Result);


                    task.Dispose();
                }, error =>
                {
                    _activeTasks.Remove(taskTitle);

                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle,
                            error.Message, TaskStatus.Faulted, _activeTasks.Count));

                    onCompleted?.Invoke(TaskStatus.Faulted, null);

                    task.Dispose();
                });

            }
        }

        public void RunOnUIAsync(Action action, string taskTitle, Action<TaskStatus> onCompleted = null)
        {

            if (_activeTasks.Contains(taskTitle))
            {

                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle
                    , Resources.Properties.Resources.AppTaskErrorMultipleTask, TaskStatus.Canceled, _activeTasks.Count));
            }
            else
            {
                _activeTasks.Add(taskTitle);
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.Running, _activeTasks.Count));
                var operation = Application.Current.Dispatcher.InvokeAsync(action);



                //Completed
                operation.Task.Await(() =>
                {
                    _activeTasks.Remove(taskTitle);

                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle,
                    string.Empty, TaskStatus.RanToCompletion, _activeTasks.Count));


                    onCompleted?.Invoke(TaskStatus.RanToCompletion);


                    operation.Task.Dispose();

                }, error =>
                {
                    _activeTasks.Remove(taskTitle);

                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle,
                            error.Message, TaskStatus.Faulted, _activeTasks.Count));

                    onCompleted?.Invoke(TaskStatus.Faulted);

                    operation.Task.Dispose();
                });

            }           


        }

    }

    public class TaskFeedbackArgs : EventArgs
    {
        public TaskFeedbackArgs(string taskTitle, string taskError, TaskStatus taskStatus, int currentlyRunningTask)
        {
            TaskTitle = taskTitle;
            TaskError = taskError;
            TaskStatus = taskStatus;
            CurrentlyRunningTask = currentlyRunningTask;
        }

        public string TaskTitle { get; }

        public string TaskError { get; }

        public TaskStatus TaskStatus { get; }

        public int CurrentlyRunningTask { get; }

    }




}
