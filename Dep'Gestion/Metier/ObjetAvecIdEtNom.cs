using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public abstract class ObjetAvecIdEtNom : ObjetAvecId
    {

        public string nom { get; set; }

        override public string ToString()
        {
            string res = ", nom=" + nom;

            return base.ToString() + res;
        }
    }
}