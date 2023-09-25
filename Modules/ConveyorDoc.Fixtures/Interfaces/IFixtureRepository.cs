using ConveyorDoc.Core;
using ConveyorDoc.Fixtures.Model;
using ConveyorDoc.Fixtures.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConveyorDoc.Fixtures.Interfaces
{
    public interface IFixtureRepository 
    {
        void Insert(FixtureRecord entity);
        void Delete(FixtureRecord entity);
        void Update(FixtureRecord entity);
        IEnumerable<FixtureRecord> GetAll();
    }
}
