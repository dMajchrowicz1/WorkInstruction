using ConveyorDoc.Core;
using ConveyorDoc.Fixtures.Interfaces;
using ConveyorDoc.Fixtures.Model;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Fixtures.ViewModels.Dialogs
{
    public class AddFixtureDialogViewModel : BindableBase, IDialogAware2
    {

        public string Title => $"{Resources.Properties.Resources.Add} {Resources.Properties.Resources.Fixtures}";

        public string PrimaryButtonText => Resources.Properties.Resources.Add;

        public event Action<IDialogResult> RequestClose;


        private FixtureRecord _fixture =  new FixtureRecord();
        public FixtureRecord Fixture
        {
            get { return _fixture; }
            set { SetProperty(ref _fixture, value); }
        }

        public FixtureViewModelBase ViewModelBase { get; }

        public IPDFCommands PDFCommands { get; }

        public AddFixtureDialogViewModel(FixtureViewModelBase viewModelBase, IPDFCommands pdfCommands)
        {
            ViewModelBase = viewModelBase;

            //Settings fixture data
            Fixture.CreatedBy = Environment.UserName;
            Fixture.CreateDate = DateTime.Now.ToString(Constants.DATA_TIME_FORMAT);
            this.PDFCommands = pdfCommands;

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public void OnPrimaryButtonPressed()
        {
            var dialogParameter = new DialogParameters();
            dialogParameter.Add("entity", Fixture);

            if (Fixture != null)
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dialogParameter));
            }
        }
    }
}
