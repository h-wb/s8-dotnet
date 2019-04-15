using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Departement : ObjetAvecIdEtNom
    {

        public List<Annee> annees;

        public Departement()
        {
            init();
        }

        public Departement(string nom)
        {
            init();
            this.nom = nom;
        }

        public Departement(string nom, List<Annee> annees)
        {
            init();
            this.nom = nom;
            this.annees = annees;
        }

        new public void init()
        {
            base.init();
            this.annees = new List<Annee>();
        }

        override public string ToString()
        {
            string res = "";
            foreach (Annee val in annees)
            {
                res += "(" + val.ToString() + ")";
            }
            return base.ToString() + res;
        }


    }
}