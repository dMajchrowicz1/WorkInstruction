

using ConveyorDoc.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace ConveyorDoc.Core
{

    public class AppTaskManager : IAppTask , IAppTaskManager, IActiveTasks
    {

        public List<string> ActiveTasks { get; } = new List<string>();

        public EventHandler<TaskFeedbackArgs> TaskStatusChanged { get; set; }

        public void Run(Action action, string taskTitle, Action<TaskStatus> onCompleted = null)
        {
            Task task = null;


            TaskStatusChanged?.Invoke(this,new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.Created));

            if (ActiveTasks.Contains(taskTitle))
            {
                TaskStatusChanged?.Invoke
                    (this, new TaskFeedbackArgs(taskTitle, Resources.Properties.Resources.AppTaskErrorMultipleTask, TaskStatus.Canceled));
            }
            else
            {
                ActiveTasks.Add(taskTitle);
                task = new Task(action);                
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.Running));
                task.Start();

                // For error handling.
                task.ContinueWith(t =>
                {
                    ActiveTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, t.Exception.Message, TaskStatus.Faulted));
                    

                    //Invok completed task callback
                    onCompleted?.Invoke(TaskStatus.Faulted);


                }, TaskContinuationOptions.OnlyOnFaulted);

                // If it succeeded.
                task.ContinueWith(t =>
                {
                    ActiveTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.RanToCompletion));
                    

                    //Invok completed task callback
                    onCompleted?.Invoke(TaskStatus.RanToCompletion);


                }, TaskContinuationOptions.OnlyOnRanToCompletion);

                task.Await(task.Dispose);

            }
        }

        public void Run(Func<object> action, string taskTitle, Action<TaskStatus, object> onCompleted = null)
        {
            Task<object> task = null;



            TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.Created));

            if (ActiveTasks.Contains(taskTitle))
            {
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, 
                    Resources.Properties.Resources.AppTaskErrorMultipleTask, 
                    TaskStatus.Canceled));
            }
            else
            {
                ActiveTasks.Add(taskTitle);
                task = new Task<object>(action);
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.Running));

                task.Start();

                // For error handling.
                task.ContinueWith(t =>
                {
                    ActiveTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, t.Exception.Message, TaskStatus.Faulted));
                    

                    //Invok completed task callback
                    onCompleted?.Invoke(TaskStatus.Faulted, task.Result);


                }, TaskContinuationOptions.OnlyOnFaulted);

                // If it succeeded.
                task.ContinueWith(t =>
                {
                    ActiveTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.RanToCompletion));
                    

                    //Invok completed task callback
                    onCompleted?.Invoke(TaskStatus.RanToCompletion, task.Result);


                }, TaskContinuationOptions.OnlyOnRanToCompletion);


                task.Await(task.Dispose);

            }
        }

        public void RunOnUIThread(Action action, string taskTitle, Action<TaskStatus> onCompleted = null)
        {

            TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.Created));

            if (ActiveTasks.Contains(taskTitle))
            {
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, Resources.Properties.Resources.AppTaskErrorMultipleTask, TaskStatus.Canceled));
            }
            else
            {
                ActiveTasks.Add(taskTitle);
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.Running));
                var operation = Application.Current.Dispatcher.InvokeAsync(action);
                
              

                // For error handling.
                operation.Task.ContinueWith(t =>                
                {
                    ActiveTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, t.Exception.Message, TaskStatus.Faulted));
                    

                    //Invok completed task callback
                    onCompleted?.Invoke(TaskStatus.Faulted);

                }
                , TaskContinuationOptions.OnlyOnFaulted);

                // If it succeeded.
                operation.Task.ContinueWith(t =>
                {
                    ActiveTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, TaskStatus.RanToCompletion));
                    

                    //Invok completed task callback
                    onCompleted?.Invoke(TaskStatus.RanToCompletion);


                }, TaskContinuationOptions.OnlyOnRanToCompletion);


            }          

        }
           

        public int GetCurrentlyRunningTasksCount()
        {
            return ActiveTasks.Count;
        }

        private void OnTaskFinished(TaskStatus status, string error, string taskTitle)
        {

            TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, error, status));
            ActiveTasks.Remove(taskTitle);
        }


    }

    public interface IActiveTasks
    {
        public List<string> ActiveTasks { get; }
    }

    public class TaskFeedbackArgs : EventArgs
    {
        public TaskFeedbackArgs(string taskTitle, string taskError, TaskStatus taskStatus)
        {
            TaskTitle = taskTitle;
            TaskError = taskError;
            TaskStatus = taskStatus;
        }

        public string TaskTitle { get; }

        public string TaskError { get; }

        public TaskStatus TaskStatus { get; }   


       
    }

}
