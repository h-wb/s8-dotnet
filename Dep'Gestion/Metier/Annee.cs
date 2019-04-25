using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Annee : ObjetAvecIdEtNom
    {
        public Departement dep { get; set; }

        public Annee()
        {
            this.init();
        }

        public Annee(string nom, Departement dep)
        {
            this.init();
            this.nom = nom;
            this.dep = dep;
        }

        public Annee(int id, string nom, Departement dep)
        {
            this.init();
            this.id = id;
            this.nom = nom;
            this.dep = dep;
        }
        
        new public void init()
        {
            base.init();
        }
    }
}