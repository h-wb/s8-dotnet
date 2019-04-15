using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public abstract class ObjetAvecId
    {
        public int id { get; set; }

        public void init()
        {
            this.id = -1;
        }


        override public string ToString()
        {

            string res = "";

            res += this.GetType().Name + ": id=" + id;

            return res;
        }
    }
}