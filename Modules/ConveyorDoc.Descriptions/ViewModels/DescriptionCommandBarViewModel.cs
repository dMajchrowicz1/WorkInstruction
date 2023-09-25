using ConveyorDoc.Descriptions.ViewModels.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Descriptions.ViewModels
{
    public class DescriptionCommandBarViewModel : BindableBase
    {

        public CommandBarCommands Commands { get; }


        public DescriptionViewModelBase ViewModelBase { get; }


        public DescriptionCommandBarViewModel(CommandBarCommands commands, DescriptionViewModelBase viewModelBase)
        {
            ViewModelBase = viewModelBase;
            Commands = commands;
            
        }
    }
}
