using DocumentFormat.OpenXml.Drawing;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Model.Settings
{
    public class InstructionSettings : BindableBase
    {
        private bool _generateDetailedTools = false;
        public bool GenerateDetailedTools
        {
            get { return _generateDetailedTools; }
            set { SetProperty(ref _generateDetailedTools, value); }
        }
    }
}
