using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core
{
    public interface IAppDirectories
    {
        string FixturesWorkspaceDir { get; }

        string DrawingTemplatesDir { get; }
    }
}
