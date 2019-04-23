using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Annee : ObjetAvecIdEtNom
    {
        public Annee()
        {
            this.init();
        }

        public Annee(string nom)
        {
            this.init();
            this.nom = nom;
        }

        public Annee(int id, string nom)
        {
            this.init();
            this.id = id;
            this.nom = nom;
        }
        
        new public void init()
        {
            base.init();
        }
    }
}