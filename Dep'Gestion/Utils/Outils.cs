using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils
{
    public class Outils
    {
        public static string generateRandomString()
        {
            return System.IO.Path.GetRandomFileName();
        }


    }
}