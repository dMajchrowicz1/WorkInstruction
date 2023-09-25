using ConveyorDoc.Fixtures.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Fixtures.Interfaces
{
    public interface ISearchCommand
    {
        public DelegateCommand<FixtureRecord> FolderBrowseCommand { get; }
    }
}
