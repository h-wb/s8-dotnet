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
        public bool hasGroups;

        public TypeCours()
        {
            base.init();
        }
        public TypeCours(string nom)
        {
            base.init();
            this.Nom = nom;
            this.hasGroups = false;
        }

        public TypeCours(string nom, bool hasGroups)
        {
            base.init();
            this.Nom = nom;
            this.hasGroups = hasGroups;
        }

        public TypeCours(int id, string nom, bool hasGroups)
        {
            base.init();
            this.Id = id;
            this.Nom = nom;
            this.hasGroups = hasGroups;
        }


        override public string ToString()
        {
            return base.ToString() + ", hasGroups=" + hasGroups;
        }

    }
}