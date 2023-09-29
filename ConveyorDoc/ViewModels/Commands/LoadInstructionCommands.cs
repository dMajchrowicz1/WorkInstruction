using ConveyorDoc.Business.Model;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.ViewModels.Commands
{
    public class LoadInstructionCommands :  ILoadInstructionCommand
    {

        private IAppTask _appTask;
        private IGetToolQuery _getToolQuery;




        private DelegateCommand<object> _loadTools;
        public DelegateCommand<object> LoadToolsCommand =>
            _loadTools ?? (_loadTools = new DelegateCommand<object>(ExecuteLoadToolsCommand));

        private DelegateCommand<object> _loadVariables;
        public DelegateCommand<object> LoadVariablesCommand =>
            _loadVariables ?? (_loadVariables = new DelegateCommand<object>(ExecuteLoadVariablesCommand));


        private DelegateCommand<object> _loadDescription;
        public DelegateCommand<object> LoadDescriptionCommand =>
            _loadDescription ?? (_loadDescription = new DelegateCommand<object>(ExecuteLoadDescriptionCommand));




        public LoadInstructionCommands(IAppTask appTask, IGetToolQuery getToolQuery)
        {
            _appTask = appTask;
            _getToolQuery = getToolQuery;
        }

        private void ExecuteLoadToolsCommand(object parameter)
        {
            if (parameter is Word word && word is not null)
            {

                _appTask.RunOnUIAsync(() =>
                {
                    word.FindProgramTools(_getToolQuery);

                }, Resources.Properties.Resources.ReloadingTools);

            }
        }


       private void ExecuteLoadDescriptionCommand(object parameter)
       {
            if (parameter is Word word && word is not null)
            {

                _appTask.RunOnUIAsync(() =>
                {
                    word.GetDefaultDescription();
                }
                    , $"{Resources.Properties.Resources.Loading}   {Resources.Properties.Resources.Description}");
            }
       }


        private void ExecuteLoadVariablesCommand(object parameter)
        {
            if (parameter is Word word && word is not null)
            {
                _appTask.RunOnUIAsync(() =>
                {
                    word.ReadVariables();
                },
                    $"{Resources.Properties.Resources.Loading} {Resources.Properties.Resources.Variables}");
            }
        }


        
    }
}
