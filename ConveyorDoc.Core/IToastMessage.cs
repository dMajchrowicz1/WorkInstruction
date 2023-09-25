using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core
{
    public interface IToastMessage
    {
        void ShowInfo(string message);

        void ShowError(string message);

        void ShowWarning(string message);

        void ShowSucces(string message);
    }
}
