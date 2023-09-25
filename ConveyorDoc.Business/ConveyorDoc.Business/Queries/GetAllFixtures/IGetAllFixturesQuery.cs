using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Business.Queries
{
    public interface IGetAllFixturesQuery
    {
        IEnumerable<FixtureDto> GetAllFixtures();
    }
}
