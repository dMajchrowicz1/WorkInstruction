using ConveyorDoc.Business.Model;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;

namespace ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels
{
    public class SubItemBase<Tmodel> : BindableBase , INavigationAware
    {
        private IWindowsDialogService _windowsDialogService;

        private Type _addDialog;



        private Word _selectedWord;
        public Word SelectedWord
        {
            get { return _selectedWord; }
            set { SetProperty(ref _selectedWord, value); }
        }

        private DelegateCommand _addItemCommand;
        public DelegateCommand AddItemCommand =>
            _addItemCommand ?? (_addItemCommand = new DelegateCommand(ExecuteAddItemCommand));


        private DelegateCommand<object> _removeItemCommand;
        public DelegateCommand<object> RemoveItemCommand =>
            _removeItemCommand ?? (_removeItemCommand = new DelegateCommand<object>(ExecuteRemoveItemCommand));


        public SubItemBase(IWindowsDialogService windowsDialogService, Type addDialog)
        {
            _windowsDialogService = windowsDialogService;

            _addDialog = addDialog;
        }



        //NavigationAware methods
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedWord = navigationContext.Parameters.GetValue<Word>("Instruction");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
            navigationContext.Parameters.Add("InstructionCallback", SelectedWord);
        }



        //Commands methods
        private void ExecuteAddItemCommand()
        {
            var dialogParameter = new DialogParameters();
            dialogParameter.Add(nameof(Word), SelectedWord);

            if(_addDialog is not null)
            {
                _windowsDialogService.ShowContentDialog(_addDialog, null, dialogParameter);
            }
           
        }


        private void ExecuteRemoveItemCommand(object parameter)
        {
            if (parameter is ToolDto tool)
            {
                SelectedWord.Tools.Remove(tool);
            }
            else if (parameter is FixtureDto fixture)
            {
                SelectedWord.Fixtures.Remove(fixture);
            }
            else if (parameter is Drawing pdfInstruction)
            {
                SelectedWord.Drawings.Remove(pdfInstruction);
            }
        }
    }
}
