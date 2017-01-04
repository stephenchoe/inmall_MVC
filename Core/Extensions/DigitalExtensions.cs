using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class DigitalExtensions
    {
        public static double RoundUp(this double input, int num)
        {
            double tmpNum = Math.Floor(input);
            if ((input - tmpNum) > 0) //判斷input是否為整數 
            {
                if ((input - Math.Round(input, num)) != 0) //判斷所要取的位數是否存在 
                {
                    //利用四捨五入的方法判斷是否要進位，若取的下一位數大於等於5則不用進位 
                    if (Convert.ToInt32(input * Math.Pow(10, num + 1) % 10) < 5)
                    {
                        return Math.Round(input, num, MidpointRounding.AwayFromZero) + Math.Pow(0.1, num);
                    }
                    else
                    {
                        return Math.Round(input, num, MidpointRounding.AwayFromZero);
                    }
                }
                else
                {
                    return input;
                }
            }
            else
            {
                return input;
            }
        }





    }
}
