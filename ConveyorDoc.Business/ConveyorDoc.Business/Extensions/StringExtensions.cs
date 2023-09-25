using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Business.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// If string is null or empty returns dash
        /// </summary>
        /// <param name="value">string</param>
        /// <returns>dash</returns>
        public static string NullToDash(this string value)
        {
            if (value is null || value == string.Empty)
            {
                value = "-";
            }

            return value;
        }


        public static bool NotNullOrEmpty(this string value)
        {
            return true ? value != null && value != string.Empty : false;
        }

        public static string OffsetFormat(this string value)
        {
            if (value != null && value.Count() == 1)
            {
                return $"0{value}";
            }
            else
                return value;
        }
    }
}
