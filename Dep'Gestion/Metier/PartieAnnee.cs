using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Metier
{
    public class PartieAnnee : ObjetBase
    {
        private Annee _annee;
        private String _description;

        public PartieAnnee()
        {
            this.init();
        }

        public PartieAnnee(string nom, Annee annee, String description)
        {
            this.init();
            this.Nom = nom;
            this.Annee = annee;
            this._description = description;
        }

        public PartieAnnee(int id, string nom, Annee annee, String description)
        {
            this.init();
            this.Id = id;
            this.Nom = nom;
            this.Annee = annee;
            this._description = description;
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

        public String Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

    }
}
