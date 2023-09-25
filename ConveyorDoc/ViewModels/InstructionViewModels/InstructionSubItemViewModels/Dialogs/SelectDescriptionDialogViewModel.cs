
using ConveyorDoc.Business.Model;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Model.Filters;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Windows.UI.Composition;

namespace ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels.Dialogs
{
    public class SelectDescriptionDialogViewModel : SelectBase<DescriptionDto, DescriptionFilter>, IDialogAware2
    {
        public string PrimaryButtonText => Resources.Properties.Resources.Add;

        public string Title => Resources.Properties.Resources.SelectDescription;

        
       


        public event Action<IDialogResult> RequestClose;       

        public override ICollectionView Collection { get; set; }

        public ObservableCollection<string> ModuleTypes { get; private set; }


        public ObservableCollection<string> Machines { get; private set; }


        public ObservableCollection<string> Sizes { get; private set; }


        public SelectDescriptionDialogViewModel(IGetAllDescriptionsQuery query) : base(new DescriptionFilter())
        {
            _queryResult = query.GetAllDescriptions();

            InitCollection();

            InitFiltersSource();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            _passedWordInstruction = parameters.GetValue<Word>(nameof(Word));
        }

        public void OnPrimaryButtonPressed()
        {
           _passedWordInstruction.Description =  String.Concat(_queryResult.Where(x => x.IsSelected == true).Select(x=>x.Text));
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, null));
        }


        //private 
        private Word _passedWordInstruction;

        private IEnumerable<DescriptionDto> _queryResult;

        private void Filter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Collection.Refresh();
        }

        private void InitFiltersSource()
        {
            ModuleTypes = new ObservableCollection<string>(_queryResult.Select(x => x.ModuleType).Distinct());
            Sizes =  new ObservableCollection<string>(_queryResult.Select(x => x.Size).Distinct());
            Machines = new ObservableCollection<string>(_queryResult.Select(x => x.Machine).Distinct());
        }

        private void InitCollection()
        {
            Collection = CollectionViewSource.GetDefaultView(_queryResult);
            Collection.Filter = new Predicate<object>(Filter.Contains);
            Filter.PropertyChanged += Filter_PropertyChanged;
        }

    }
}
