using ConveyorDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Interfaces.Services
{
    public interface IAppTaskManager
    {
        EventHandler<TaskFeedbackArgs> TaskStatusChanged { get; set; }

        

    }
}
