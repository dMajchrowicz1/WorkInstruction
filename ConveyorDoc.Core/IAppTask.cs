using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core
{
    public interface IAppTask
    {
        void RunAsync(Action action, string taskTitle, Action<TaskStatus> onCompleted = null);

        void RunAsync(Func<object> action, string taskTitle, Action<TaskStatus, object> onCompleted = null);

        void RunOnUIAsync(Action action, string taskTitle, Action<TaskStatus> onCompleted = null);


    }
}
