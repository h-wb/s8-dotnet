using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier 
{
    public class InfosAssignation : ObjetBase
    {
        private EC _ec;
        private TypeCours _typeCours;
        private Enseignant _enseignant;
        private double _nbHeures;
        public ObservableCollectionExt<Enseignant> _enseignants;

        public EC EC
        {
            get { return _ec; }
            set { SetProperty(ref _ec, value); }
        }

        public TypeCours TypeCours
        {
            get { return _typeCours; }
            set { SetProperty(ref _typeCours, value); }
        }

        public Enseignant Enseignant
        {
            get { return _enseignant; }
            set { SetProperty(ref _enseignant, value); }
        }

        public Double NbHeures
        {
            get { return _nbHeures; }
            set { SetProperty(ref _nbHeures, value); }
        }

        public InfosAssignation()
        {
            this.init();
        }

        new public void init()
        {
            base.init();
        }

        public InfosAssignation(int id, string nom, EC ec, TypeCours typeCours, Enseignant enseignant, double nbHeures)
        {
            this.init();
            this._id = id;
            this._nom = nom;
            this._ec = ec;
            this._typeCours = typeCours;
            this._enseignant = enseignant;
            this._nbHeures = nbHeures;
        }

        public InfosAssignation(string nom, EC ec, TypeCours typeCours, Enseignant enseignant, double nbHeures)
        {
            this.init();
            this._nom = nom;
            this._ec = ec;
            this._typeCours = typeCours;
            this._enseignant = enseignant;
            this._nbHeures = nbHeures;
        }

        public ObservableCollectionExt<Enseignant> Enseignants
        {
            get { return _enseignants; }
            set { SetProperty(ref _enseignants, value); }
        }

    
    }
}
