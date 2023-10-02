using ConveyorDoc.Business.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.ViewModels.InstructionViewModels
{
    public class InstructionViewModelBase : BindableBase
    {
       
        private Instruction _currentInstruction;
        public Instruction CurrentInstruction
        {
            get { return _currentInstruction; }
            set { SetProperty(ref _currentInstruction, value); }
        }

    }
}
