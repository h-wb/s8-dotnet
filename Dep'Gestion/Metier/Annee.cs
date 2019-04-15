using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Annee : ObjetAvecIdEtNom
    {

        //semestres ou autre type de répartition
        public List<PartieAnnee> partiesAnnees;


        public Annee()
        {
            this.init();
        }

        public Annee(string nom)
        {
            this.init();
            this.nom = nom;
        }

        public Annee(string nom, List<PartieAnnee> partiesAnnees)
        {
            this.init();
            this.nom = nom;
            this.partiesAnnees = partiesAnnees;
        }

        new public void init()
        {
            base.init();
            this.partiesAnnees = new List<PartieAnnee>();
        }



    }
}