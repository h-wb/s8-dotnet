using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class PartieAnnee : ObjetAvecIdEtNom
    {
        public List<Enseignement> enseignements;
        public PartieAnnee()
        {
            this.init();
        }

        public PartieAnnee(string nom)
        {
            this.init();
            this.nom = nom;
        }

        public PartieAnnee(string nom, List<Enseignement> enseignements)
        {
            this.init();
            this.nom = nom;
            this.enseignements = enseignements;
        }
    }
}