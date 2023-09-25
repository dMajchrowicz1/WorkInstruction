using ConveyorDoc.Business.Model;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Fixtures.Interfaces;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Interfaces.Commands;
using ConveyorDoc.Model;
using ConveyorDoc.Model.Filters;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels.Dialogs
{
    public class SelectFixtureDialogViewModel : SelectBase<FixtureDto, FixtureFilter> , IDialogAware2
    {

        public string PrimaryButtonText => Resources.Properties.Resources.Add;
        public string Title => Resources.Properties.Resources.SelectFixtures;





        public event Action<IDialogResult> RequestClose;

        public override ICollectionView Collection { get; set; }

        public ObservableCollection<string> ItemTypes { get; private set; }

        public ObservableCollection<string> Sizes { get; private set; }

        public ObservableCollection<string> Machines { get; private set; }

        public IApplicationCommands ApplicationCommands { get; }


        public SelectFixtureDialogViewModel
            (IGetAllFixturesQuery query,IApplicationCommands applicationCommands) :base(new FixtureFilter())
        {
            ApplicationCommands = applicationCommands;

            _queryResult = query.GetAllFixtures();

            InitCollection();

            InitFiltersSource();
        }



        public void OnPrimaryButtonPressed()
        {
            _passedWordInstruction.Fixtures.AddRange(_queryResult.Where(x=>x.IsSelected == true));
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, null));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            _passedWordInstruction = parameters.GetValue<Word>(nameof(Word));
        }


        //private
        private Word _passedWordInstruction;

        private IEnumerable<FixtureDto> _queryResult;

        private void Filter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Collection.Refresh();
        }

        private void InitCollection()
        {

            Collection = CollectionViewSource.GetDefaultView(_queryResult);
            Collection.Filter = new Predicate<object>(Filter.Contains);
            Filter.PropertyChanged += Filter_PropertyChanged;
        }

        private void InitFiltersSource()
        {
            ItemTypes = new ObservableCollection<string>(_queryResult.Select(x => x.ItemType).Distinct());
            Sizes = new ObservableCollection<string>(_queryResult.Select(x => x.Size).Distinct());
            Machines = new ObservableCollection<string>(_queryResult.Select(x => x.Machine).Distinct());
        }

    }
}
