using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class IntegerExtentions
    {
        public static string ToMonthString(this int input)
        {
            if (input < 10) return "0" + input.ToString();
            return input.ToString();

        }

    }
}
