using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ZTC.Models.Extensions
{
    public static class Ext
    {
        public static string ToBrDecimal(this decimal d)
        {
            var cultBr = new CultureInfo("pt-BR");
             string result = "0,00";

            try
            {
                if(d != 0)
                    result = String.Format(cultBr, "{0:##,#.00}", d);
            }
            catch
            {
                result = "0,00";
            }

            return result;
        }
        public static string ToYesNoString(this bool value)
        {
            return value ? "Sim" : "Não";
        }
    }
}