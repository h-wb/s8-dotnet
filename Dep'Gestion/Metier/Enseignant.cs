using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Enseignant : ObjetAvecIdEtNom
    {
        public string prenom { get; set; }
        public Categorie categorie { get; set; }
        public double nbHeuresTravaillees { get; set; }

        public Enseignant(int id, string prenom, string nom, Categorie categorie, double nbHeuresTravaillees)
        {
            this.init();
            this.id = id;
            this.prenom = prenom;
            this.nom = nom;
            this.categorie = categorie;
            this.nbHeuresTravaillees = nbHeuresTravaillees;
        }


        public Enseignant(string prenom, string nom)
        {
            this.init();
            this.nom = nom;
            this.prenom = prenom;
        }

        public Enseignant(string prenom, string nom, Categorie categorie)
        {
            this.init();
            this.nom = nom;
            this.prenom = prenom;
            this.categorie = categorie;
        }

        new public void init()
        {
            base.init();
            this.nbHeuresTravaillees = 0;
        }

        public override string ToString()
        {
            string categString = "";

            if (this.categorie != null)
            {
                categString = this.categorie.ToString();
            }
            return base.ToString() + ", prenom = " + this.prenom + ", categorie = " + categString + ", nb d'heures travaillée=" + this.nbHeuresTravaillees;
        }
    }
}