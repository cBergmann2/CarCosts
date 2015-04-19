using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CarCosts
{
    public static class HelperFunctions
    {
        public static bool convertStringToDouble(String str, ref double value)
        {
            try
            {
                value = Convert.ToDouble(str);
                return true;
            }
            catch (FormatException e)
            {
                //checked if mobile language is german
                CultureInfo ci = new CultureInfo(Windows.System.UserProfile.GlobalizationPreferences.Languages[0]);
                
                //Transform german String                
                //German String can 

                if (str.Count(x => x == ',') <= 1)
                {                                        
                    //Replace possible ',' with '.'
                    if (str.Contains(','))
                    {
                        str.Replace(',', '.');
                    }

                    try
                    {
                        value = Convert.ToDouble(str);
                        return true;
                    }
                    catch (FormatException ex) { }
                }
            }
            return false;
        }

    }
}
