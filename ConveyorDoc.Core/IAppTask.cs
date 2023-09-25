using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core
{
    public interface IAppTask
    {
        void Run(Action action, string taskTitle, Action<TaskStatus> onCompleted = null);

        void Run(Func<object> action, string taskTitle, Action<TaskStatus, object> onCompleted = null);

        void RunOnUIThread(Action action, string taskTitle, Action<TaskStatus> onCompleted = null);


    }
}
