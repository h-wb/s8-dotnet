using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class EC : ObjetAvecIdEtNom
    {
        public List<Enseignant> enseignants { get; set; }

        public List<Cours> cours { get; set; }


        public EC()
        {
            this.init();
        }

        public EC(string nom)
        {
            this.init();
            this.nom = nom;
        }

        //On crée un EC avec des cours, donc les profs de cet EC sont le/les prof(s) de ces cours.
        //Dans ce cas, on n'a pas forcément de prof assigné à l'EC entier, les profs de l'EC seront les profs de ces cours.
        public EC(string nom, List<Cours> cours)
        {
            this.init();
            this.nom = nom;
            this.cours = cours;
            this.updateEnseignants();
        }

        //On crée un EC composé de cours prédéfinis, avec un enseignant qui sera assignés à ceux-ci
        public EC(string nom, List<Cours> cours, Enseignant enseignant)
        {
            this.init();
            this.nom = nom;
            this.cours = cours;
            this.assignerEnseignant(enseignant);
        }


        new public void init()
        {
            base.init();
            this.enseignants = new List<Enseignant>();
            this.cours = new List<Cours>();
        }

        //Lorsqu'on assigne un Enseignant à un EC, ça signifie qu'il prend en charge tous les Cours de cet EC
        public void assignerEnseignant(Enseignant enseignant)
        {
            enseignants.Clear();
            this.enseignants.Add(enseignant);

            foreach (Cours cours in this.cours)
            {
                cours.assignerEnseignant(enseignant);
            }
        }



        //Met à jour la liste d'enseignants de l'EC, selon les enseignants de chaque cours composant cet EC.
        public void updateEnseignants()
        {
            if (this.cours.Any())
            {
                this.enseignants.Clear();
                foreach (Cours cours in this.cours)
                {
                    this.enseignants.AddRange(cours.getEnseignants());
                    cours.updateEnseignants();
                    this.enseignants.AddRange(cours.enseignants);
                }
            }
        }
    }
}