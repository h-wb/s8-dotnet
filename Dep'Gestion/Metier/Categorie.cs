using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    //Représente une catégorie d'enseignant, ex: Maître de conférence, enseignant chercher, etc.
    public class Categorie : ObjetAvecIdEtNom
    {
        //Exemple: un maître de conférence doit donner 300h de cours en equivalent TD
        public double heuresATravailler { get; set; }

        public Categorie()
        {
            base.init();
        }

        public Categorie(string nom, double heuresATravailler)
        {
            base.init();
            this.nom = nom;
            this.heuresATravailler = heuresATravailler;
        }

        public Categorie(int id, string nom, double heuresATravailler)
        {
            base.init();
            this.id = id;
            this.nom = nom;
            this.heuresATravailler = heuresATravailler;
        }


        public override string ToString()
        {
            return base.ToString() + ", heures à travailler = " + this.heuresATravailler;
        }

    }
}