using ConveyorDoc.Core;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Model.Settings
{
    public class DirectoriesSettings : BindableBase, IAppDirectories
    {

        private string _drawingTemplatesDir = string.Empty;
        public string DrawingTemplatesDir
        {
            get { return _drawingTemplatesDir; }
            set { SetProperty(ref _drawingTemplatesDir, value); }
        }

        private string _fixturesWorkspaceDir = string.Empty;
        public string FixturesWorkspaceDir
        {
            get { return _fixturesWorkspaceDir; }
            set { SetProperty(ref _fixturesWorkspaceDir, value); }
        }
    }
}
