using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    //Représente un groupe de cours et pas forcément un seul cours. Un cours = 1H. 
    //Exemple, que l'on souhaite créer 1 ou 10 TP, on utilise cette classe
    public class Cours : ObjetBase
    {
        //L'enseignant responsable de ce cours, s'il n'est pas divisé en groupes 
        public Enseignant enseignant
        {
            get
            {
                return enseignant;
            }
            set
            {
                if (this.typeCours != null)
                {
                    if (!this.typeCours.hasGroups)
                    {
                        enseignant = value;
                    }
                }
                else
                {
                    throw new Exception("Le/les enseignants de cours pouvant être divisés en groupes sont accessibles via les groupes.");
                }
            }
        }

        //Un seul enseignant si le type de cours ne peut pas avoir de groupes (ex:CM), sinon 1 enseignant par groupe au max
        public List<Enseignant> enseignants { get; set; }

        //Le nombre de cours, exemple: 10 CM, 1 TP, 5 TD, etc.
        public int nbCours { get; set; }

        //Les groupes pour ces cours. Exemple: 10 TP divisés en quatre groupes.
        public List<Groupe> groupes
        {
            get
            {
                return groupes;
            }
            set
            {
                //On peut diviser ce cours en groupes seulement si son type le permet
                if (this.typeCours != null)
                {
                    if (this.typeCours.hasGroups)
                    {
                        groupes = value;
                    }
                    else
                    {
                        throw new Exception("ce cours ne peut pas être divisé en groupes");
                    }
                }

            }
        }

        public TypeCours typeCours { get; set; }

        public Cours(string nom, int nbCours, TypeCours typeCours)
        {
            this.init();
            this.Nom = nom;
            this.typeCours = typeCours;
            this.nbCours = nbCours;
        }

        public Cours(string nom, int nbCours, TypeCours typeCours, List<Groupe> groupes)
        {
            this.init();
            this.Nom = nom;
            this.typeCours = typeCours;
            this.nbCours = nbCours;
            this.groupes = groupes;
        }

        //Méthode pour récupérer le/les enseignants de ce cours, et des groupes éventuels associés à celui-ci.
        public List<Enseignant> getEnseignants()
        {
            List<Enseignant> res = new List<Enseignant>();
            if (this.groupes.Any())
            {
                foreach (Groupe groupe in this.groupes)
                {
                    res.Add(groupe.enseignant);
                }
            }
            else
            {
                res.Add(this.enseignant);
            }

            return res;
        }

        new public void init()
        {
            base.init();
            this.enseignants = new List<Enseignant>();
        }




        //Vérifie les professeurs de chaque groupe et ajoute chaque professeur différent dans la liste des enseignants de ce cours
        public void updateEnseignants()
        {
            if (this.groupes.Any())
            {
                this.enseignants.Clear();

                foreach (Groupe groupe in this.groupes)
                {
                    if (!this.enseignants.Contains(groupe.enseignant))
                    {
                        this.enseignants.Add(groupe.enseignant);
                    }
                }
            }
        }


        //On assigne un enseignant à ce cours, c'est-à-dire à tous les groupes s'il est composé de groupes.
        //Cet enseignant en remplace tout autre.
        public void assignerEnseignant(Enseignant enseignant)
        {
            this.enseignants.Clear();
            this.enseignants.Add(enseignant);

            if (this.groupes.Any())
            {
                foreach (Groupe groupe in this.groupes)
                {
                    groupe.enseignant = enseignant;
                }
            }
            else
            {
                this.enseignant = enseignant;
            }
        }


    }
}