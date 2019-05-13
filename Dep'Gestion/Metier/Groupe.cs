using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    //Représente un groupe de TD, TP ou autre
    public class Groupe : ObjetBase
    {
        //L'enseignant pour ce groupe
        public Enseignant enseignant;
        public int idCours;

        public Groupe()
        {
            this.init();
        }

        public Groupe(string nom)
        {
            this.init();
            this.Nom = nom;
        }

        public Groupe(Enseignant enseignant)
        {
            this.init();
            this.enseignant = enseignant;
        }

        public Groupe(string nom, Enseignant enseignant)
        {
            this.init();
            this.idCours = -1;
            this.Nom = nom;
            this.enseignant = enseignant;
        }

        public Groupe(string nom, Enseignant enseignant, int idCours)
        {
            this.init();
            this.idCours = idCours;
            this.Nom = nom;
            this.enseignant = enseignant;
        }
    }
}