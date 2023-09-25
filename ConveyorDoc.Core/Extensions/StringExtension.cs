using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core.Extension
{
    public static class StringExtension
    {

        public static bool Filter(this string element, string value)
        {

            if (value != null && element != null)
                return element.Contains(value, StringComparison.CurrentCultureIgnoreCase);
            else if (value == null && element != null)
                return false;
            else
                return false;
        }
    }
}
