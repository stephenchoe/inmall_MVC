using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Extensions;

namespace BLL
{
    public static class HelperExtensions
    {
        public static DateTime ConvertToTaipeiTime(this DateTime input)
        {
            return DateTimeExtensions.ConvertToTaipeiTime(input);

        }
    }
}
