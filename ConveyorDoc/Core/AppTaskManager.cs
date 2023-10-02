

using ConveyorDoc.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace ConveyorDoc.Core
{

    public class AppTaskManager : IAppTask , IAppTaskManager
    {

        private List<string> _activeTasks { get; } = new List<string>();

        public EventHandler<TaskFeedbackArgs> TaskStatusChanged {get; set;}



        public void RunAsync(Action action, string taskTitle, Action<TaskStatus> onCompleted = null)
        {
            Task task = null;
            TaskStatus status = TaskStatus.Created;
            
            
            if (_activeTasks.Contains(taskTitle))
            {
                TaskStatusChanged?.Invoke
                    (this, new TaskFeedbackArgs(taskTitle, Resources.Properties.Resources.AppTaskErrorMultipleTask, 
                    status = TaskStatus.Canceled, _activeTasks.Count));
            }
            else
            {
                _activeTasks.Add(taskTitle);
                task = new Task(action);
                task.Start();
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, status = TaskStatus.Running, _activeTasks.Count));
                


                // For error handling.
                task.ContinueWith(t =>
                {
                    _activeTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, t.Exception.Message, status = TaskStatus.Faulted, _activeTasks.Count));
                   
                }, TaskContinuationOptions.OnlyOnFaulted);

                // If it succeeded.
                task.ContinueWith(t =>
                {
                    _activeTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, status = TaskStatus.RanToCompletion, _activeTasks.Count));                  

                }, TaskContinuationOptions.OnlyOnRanToCompletion);

                //Completed
                task.Await(()=>
                {                    

                    onCompleted?.Invoke(status);

                    task.Dispose();                    
                });
                
            }
        }

        public void RunAsync(Func<object> action, string taskTitle, Action<TaskStatus, object> onCompleted = null)
        {
            
            Task<object> task = null;
            TaskStatus status = TaskStatus.Created;


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
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty,status = TaskStatus.Running,_activeTasks.Count));
                task.Start();


                // For error handling.
                task.ContinueWith(t =>
                {
                    _activeTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, t.Exception.Message,status = TaskStatus.Faulted, _activeTasks.Count));
                    
                }, TaskContinuationOptions.OnlyOnFaulted);

                // If it succeeded.
                task.ContinueWith(t =>
                {
                    _activeTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty, status = TaskStatus.RanToCompletion,_activeTasks.Count));
                    
                }, TaskContinuationOptions.OnlyOnRanToCompletion);


                //Completed
                task.Await(() =>
                {
                   
                    onCompleted?.Invoke(status, task.Result);

                    task.Dispose();
                });

            }
        }

        public void RunOnUIAsync(Action action, string taskTitle, Action<TaskStatus> onCompleted = null)
        {
            
            TaskStatus status = TaskStatus.Created;


            if (_activeTasks.Contains(taskTitle))
            {
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, Resources.Properties.Resources.AppTaskErrorMultipleTask,status = TaskStatus.Canceled, _activeTasks.Count));
            }
            else
            {
                _activeTasks.Add(taskTitle);
                TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty,status = TaskStatus.Running, _activeTasks.Count));
                var operation = Application.Current.Dispatcher.InvokeAsync(action);
                              

                // For error handling.
                operation.Task.ContinueWith(t =>                
                {
                    _activeTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, t.Exception.Message,status = TaskStatus.Faulted, _activeTasks.Count));                   
                }
                , TaskContinuationOptions.OnlyOnFaulted);

                // If it succeeded.
                operation.Task.ContinueWith(t =>
                {
                    _activeTasks.Remove(taskTitle);
                    TaskStatusChanged?.Invoke(this, new TaskFeedbackArgs(taskTitle, string.Empty,status = TaskStatus.RanToCompletion, _activeTasks.Count));
                   
                }, TaskContinuationOptions.OnlyOnRanToCompletion);


                //Invok completed task callback
                operation.Task.Await(() =>
                {
                   
                    onCompleted?.Invoke(status);

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
