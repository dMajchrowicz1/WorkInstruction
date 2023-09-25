using ConveyorDoc.Descriptions.Model;
using System.Collections.Generic;

namespace ConveyorDoc.Descriptions.Interfaces
{
    public interface IDescriptionRepository
    {
        void Delete(DescriptionRecord entity);
        IEnumerable<DescriptionRecord> GetAll();
        void Insert(DescriptionRecord entity);
        void Update(DescriptionRecord entity);
    }
}