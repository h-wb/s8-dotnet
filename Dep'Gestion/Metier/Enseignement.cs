using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Enseignement : ObjetAvecIdEtNom
    {
        public PartieAnnee partAnnee { get; set; }

        public Enseignement(int id, string nom, PartieAnnee partAnnee)
        {
            this.init();
            this.id = id;
            this.nom = nom;
            this.partAnnee = partAnnee;
        }

        public Enseignement(string nom, PartieAnnee partAnnee)
        {
            this.init();
            this.nom = nom;
            this.partAnnee = partAnnee;
        }

        new public void init()
        {
            base.init();
        }
    }
}