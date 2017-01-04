using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class IListStringExtentions
    {
        public static string GetString(this IList<string> listString)
        {
            if (listString.IsNullOrEmpty()) return "";

            string returnValue = "";
            foreach (string item in listString)
            {
                returnValue += item + ",";
            }
            if (returnValue == "") return returnValue;
            return returnValue.Remove(returnValue.Length - 1);
        }


        public static string[] GetItems(this string input)
        {
            return input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
