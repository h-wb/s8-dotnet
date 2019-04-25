using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Departement : ObjetAvecIdEtNom
    {
        public Departement dep { get; set; }

        public Departement()
        {
            init();
        }

        public Departement(int id, string nom, Departement dep)
        {
            init();
            this.id = id;
            this.nom = nom;
            this.dep = dep;
        }

        public Departement(string nom, Departement dep)
        {
            init();
            this.nom = nom;
            this.dep = dep;
        }
        

        new public void init()
        {
            base.init();
        }


    }
}