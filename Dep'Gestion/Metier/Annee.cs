using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Annee : ObjetBase
    {
        public Departement _departement;

        public Annee()
        {
            this.init();
        }

        public Annee(string nom, Departement dep)
        {
            this.init();
            this.Nom = nom;
            this._departement = dep;
        }

        public Annee(int id, string nom, Departement dep)
        {
            this.init();
            this.Id = id;
            this.Nom = nom;
            this._departement = dep;
        }
        
        new public void init()
        {
            base.init();
        }

        public Departement Departement
        {
            get { return _departement; }
            set { SetProperty(ref _departement, value); }
        }

    }
}