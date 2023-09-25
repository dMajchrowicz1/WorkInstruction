using ConveyorDoc.Business.Model;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Business.Extension;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Model;
using ConveyorDoc.Model.Filters;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels.Dialogs
{
    public class SelectToolDialogViewModel : SelectBase<ToolDto,ToolFilter>, IDialogAware2
    {

        public string PrimaryButtonText => Resources.Properties.Resources.Add;
        public string Title => Resources.Properties.Resources.SelectTools;




        public event Action<IDialogResult> RequestClose;


        public ObservableCollection<string> Offsetes { get; private set; }

        public ObservableCollection<string> Types { get; private set; }

        public ObservableCollection<string> Machines { get; private set; }

        public override ICollectionView Collection { get; set; }

        public IApplicationCommands ApplicationCommands { get; }


        //Ctor
        public SelectToolDialogViewModel(IGetAllToolsQuery query, IApplicationCommands appCommands) :base(new ToolFilter())
        {
            ApplicationCommands = appCommands;

            _queryResult = new ObservableCollection<ToolDto>(query.GetAllTools());

            InitCollection();

            InitFiltersSource();
        }



        public void OnDialogOpened(IDialogParameters parameters)
        {
            _passedWordInstruction = parameters.GetValue<Word>(nameof(Word));
        }

        public void OnPrimaryButtonPressed()
        {

            _passedWordInstruction.Tools.AddRange(_queryResult.Where(x=>x.IsSelected == true));
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, null));
        }



        //private
        private Word _passedWordInstruction;

        private ObservableCollection<ToolDto> _queryResult;

        private void Filter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Collection.Refresh();
        }

        private void InitFiltersSource()
        {
            Machines = new ObservableCollection<string>(_queryResult.Select(x => x.Machine).Distinct());

            Offsetes = new ObservableCollection<string>(_queryResult.Select(x => x.Offset).Distinct());

            Types = new ObservableCollection<string>(_queryResult.Select(x => x.Type).Distinct());
        }

        private void InitCollection()
        {
            Collection = CollectionViewSource.GetDefaultView(_queryResult);
            Collection.Filter = new Predicate<object>(Filter.Contains);
            Filter.PropertyChanged += Filter_PropertyChanged;
        }

    }
}
