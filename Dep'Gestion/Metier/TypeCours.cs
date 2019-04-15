using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class TypeCours : ObjetAvecIdEtNom
    {
        //vrai: ce type de cours peut être composé de plusieurs groupes (ex: TD, TP), faux sinon (ex: CM)
        public bool hasGroups;
        public TypeCours(string nom)
        {
            base.init();
            this.nom = nom;
            this.hasGroups = false;
        }

        public TypeCours(string nom, bool hasGroups)
        {
            base.init();
            this.nom = nom;
            this.hasGroups = hasGroups;
        }

        public TypeCours(int id, string nom, bool hasGroups)
        {
            base.init();
            this.id = id;
            this.nom = nom;
            this.hasGroups = hasGroups;
        }

        override public string ToString()
        {
            return base.ToString() + ", hasGroups=" + hasGroups;
        }

    }
}