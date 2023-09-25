using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core.Extension
{
    public static class ObservableCollectionExtension
    {
        public static void Replace<T>(this ObservableCollection<T> collection, IEnumerable<T> value)
        {
            collection.Clear();
            collection.AddRange(value);
        }

    }
}
