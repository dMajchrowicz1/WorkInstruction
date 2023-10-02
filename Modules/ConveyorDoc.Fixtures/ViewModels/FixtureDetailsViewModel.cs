using ConveyorDoc.Core;
using ConveyorDoc.Fixtures.Interfaces;
using ConveyorDoc.Fixtures.Model;
using ConveyorDoc.Fixtures.Repository;
using ConveyorDoc.Fixtures.ViewModels.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Fixtures.ViewModels
{
    public class FixtureDetailsViewModel : BindableBase 
    {



        private FixtureViewModelBase _viewModelBase;
        public FixtureViewModelBase ViewModelBase
        {
            get { return _viewModelBase; }
            set { SetProperty(ref _viewModelBase, value); }
        }


        public IPDFCommands PDFCommands { get; }


        public FixtureDetailsViewModel(FixtureViewModelBase viewModelBase, 
            IPDFCommands pdfCOmmands)
        {
            _viewModelBase = viewModelBase;

            PDFCommands = pdfCOmmands;

        }


    }
}
