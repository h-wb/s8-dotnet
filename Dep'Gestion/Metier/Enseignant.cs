﻿using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Enseignant : ObjetBase
    {

        public Categorie _categorie;
        public double nbHeuresTravaillees { get; set; }
        public string lienImage { get; set; }

        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<Categorie> categ = factoSQL.getCategorieDAO();
        private static DAO<EnseignementEnseignant> ense = factoSQL.getEnseignementEnseignantDAO();

        public Enseignant() {
            this.init();
        }

        public Enseignant(int id, string nom, string prenom, double nbHeuresTravaillees, Categorie categorie)
        {
            this.init();
            this.Id = id;
            this.Prenom = prenom;
            this.Nom = nom;
            this._categorie = categorie;
            this.nbHeuresTravaillees = nbHeuresTravaillees;
            this.lienImage = "";
        }

        public Enseignant(int id, string nom, string prenom, double nbHeuresTravaillees, Categorie categorie, string lienImage)
        {
            this.init();
            this.Id = id;
            this.Prenom = prenom;
            this.Nom = nom;
            this.Categorie = categorie;
            this.nbHeuresTravaillees = nbHeuresTravaillees;
            this.lienImage = lienImage;
        }

        public Enseignant(string nom, string prenom)
        {
            this.init();
            this.Nom = nom;
            this.Prenom = prenom;
            this.lienImage = "";
        }

        public Enseignant(string nom, string prenom, string lienImage)
        {
            this.init();
            this.Nom = nom;
            this.Prenom = prenom;
            this.lienImage = lienImage;
        }

        public Enseignant(string nom, string prenom, Categorie categorie)
        {
            this.init();
            this.Nom = nom;
            this.Prenom = prenom;
            this.Categorie = categorie;
            this.lienImage = "";
        }

        public Enseignant(string nom, string prenom, Categorie categorie, string lienImage)
        {
            this.init();
            this.Nom = nom;
            this.Prenom = prenom;
            this.Categorie = categorie;
            this.lienImage = lienImage;
        }

        new public void init()
        {
            base.init();
            this.nbHeuresTravaillees = 0;
            this._categorie = categ.find(1);
        }



        public Categorie Categorie
        {
            get { return _categorie; }
            set { SetProperty(ref _categorie, value); }
        }

        public void ajouterEnseignement(Enseignement enseignement)
        {
            ense.create(new EnseignementEnseignant(enseignement, this));
        }

        override public string ToString()
        {
   
            return (Nom + ' ' + Prenom);
        }
    }
}