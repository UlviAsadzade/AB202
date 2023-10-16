using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstarct_Interface.Extensions
{
    internal static class Helper
    {
        public static string Capitalize(this string str)
        {
            str= str.Trim();
            return str.Substring(0,1).ToUpper() + str.Substring(1).ToLower();
        }
    }
}
