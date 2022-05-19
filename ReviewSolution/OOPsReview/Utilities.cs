using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview.Data
{
    public static class Utilities
    {
        // static classes are NOT instantiated by the outside user (developer)
        // static class items are referenced using classname.xxxx
        // methods within this class have the keyword static in their signature
        // static calsses are shared between all outside user at the SAME time 
        // DO NOT consider saving data within a static class BECAUSE you cannot be certain it will be there when you use the class again 
        // consider placing GENERIC reusable methods within a static class

        // Same as overloaded methods
        // the method signatures are different
        public static bool IsZeropositive(int value)
        {
            bool valid = true; 
            if (value < 0)
            {
                valid = false;
            }
            return valid;
        }
        public static bool IsZeropositive(double value)
        {
            bool valid = true;
            if (value < 0)
            {
                valid = false;
            }
            return valid;
        }
        public static bool IsZeropositive(decimal value)
        {
            bool valid = true;
            if (value < 0)
            {
                valid = false;
            }
            return valid;
        }
    }
}
