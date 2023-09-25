using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Interfaces
{
    public interface IApplicationCommands
    {

        DelegateCommand<string> OpenPDFCommand { get; }


        /// <summary>
        /// Run's Folder browse dialog 
        /// Command Paramater has to be TextBox. Selected path will binded to property Text
        /// </summary>
        DelegateCommand<object> SearchCommand { get; }
    }
}
