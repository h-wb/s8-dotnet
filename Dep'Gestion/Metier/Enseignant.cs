using DAO;
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

        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Categorie> categ = factoSQL.getCategorieDAO();

        public Enseignant(int id, string nom, string prenom, double nbHeuresTravaillees, Categorie categorie)
        {
            this.init();
            this.id = id;
            this.prenom = prenom;
            this.nom = nom;
            this.categorie = categorie;
            this.nbHeuresTravaillees = nbHeuresTravaillees;
        }


        public Enseignant(string nom, string prenom)
        {
            this.init();
            this.nom = nom;
            this.prenom = prenom;
        }

        public Enseignant(string nom, string prenom, Categorie categorie)
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
            this.categorie = categ.find(1);
        }
    }
}