using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class TypeCours : ObjetBase
    {
        //vrai: ce type de cours peut être composé de plusieurs groupes (ex: TD, TP), faux sinon (ex: CM)
        public int _groupes;

        public TypeCours()
        {
            base.init();
        }
        public TypeCours(string nom)
        {
            base.init();
            this.Nom = nom;
            //this.Groupes = false;
        }

        public TypeCours(string nom, int hasGroups)
        {
            base.init();
            this.Nom = nom;
            this.Groupes = hasGroups;
        }

        public TypeCours(int id, string nom, int hasGroups)
        {
            base.init();
            this.Id = id;
            this.Nom = nom;
            this.Groupes = hasGroups;
        }

        public int Groupes
        {
            get { return _groupes; }
            set { SetProperty(ref _groupes, value); }
        }



        override public string ToString()
        {
            return base.ToString() + ", hasGroups=" + Groupes;
        }

    }
}