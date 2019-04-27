using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Departement : ObjetBase
    {
        public Departement()
        {
            init();
        }

        public Departement(int id, string nom)
        {
            init();
            this.Id = id;
            this.Nom = nom;
        }

        public Departement(string nom)
        {
            init();
            this.Nom = nom;
        }
        

        new public void init()
        {
            base.init();
        }


    }
}