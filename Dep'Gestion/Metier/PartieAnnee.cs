using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class PartieAnnee : ObjetBase
    {
        private Annee _annee;

        public PartieAnnee()
        {
            this.init();
        }

        public PartieAnnee(string nom, Annee annee)
        {
            this.init();
            this.Nom = nom;
            this.Annee = annee;
        }

        public PartieAnnee(int id, string nom, Annee annee)
        {
            this.init();
            this.Id = id;
            this.Nom = nom;
            this.Annee = annee;
        }

        new public void init()
        {
            base.init();
        }

        public Annee Annee
        {
            get { return _annee; }
            set { SetProperty(ref _annee, value); }
        }
    }
}
