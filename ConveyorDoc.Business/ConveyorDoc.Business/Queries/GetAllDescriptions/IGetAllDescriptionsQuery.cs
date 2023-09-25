using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Business.Queries
{
    public interface IGetAllDescriptionsQuery
    {
        IEnumerable<DescriptionDto> GetAllDescriptions();
    }
}
