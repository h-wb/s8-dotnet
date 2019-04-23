using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class PartieAnnee : ObjetAvecIdEtNom
    {
        public Annee annee { get; set; }

        public PartieAnnee()
        {
            this.init();
        }

        public PartieAnnee(string nom, Annee annee)
        {
            this.init();
            this.nom = nom;
            this.annee = annee;
        }

        public PartieAnnee(int id, string nom, Annee annee)
        {
            this.init();
            this.id = id;
            this.nom = nom;
            this.annee = annee;
        }

        new public void init()
        {
            base.init();
        }
    }
}
