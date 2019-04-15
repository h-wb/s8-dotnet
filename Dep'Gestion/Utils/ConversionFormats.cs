using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils
{
    public class ConversionFormats
    {
        public static int convert(bool val)
        {
            return val ? 1 : 0;
        }

        public static bool convert(int val)
        {
            return val == 1 ? true : false;
        }
    }
}