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
        public String _description;

        public Annee()
        {
            this.init();
        }

        public Annee(string nom, Departement dep, String description)
        {
            this.init();
            this.Nom = nom;
            this._departement = dep;
            this._description = description;
        }

        public Annee(int id, string nom, Departement dep, String description)
        {
            this.init();
            this.Id = id;
            this.Nom = nom;
            this._departement = dep;
            this._description = description;
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

        public String Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
    }
}