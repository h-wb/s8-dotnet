using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Enseignement : ObjetAvecIdEtNom
    {
        //Un enseignement est composé d'un ou plusieurs EC
        public List<EC> ECs { get; set; }

        //Un enseignement est tenu par un ou plusieurs professeurs. 
        //Par exemple, le même professeur peut enseigner tous les cours de tous les EC de l'Enseignement
        //, ou bien un prof peut s'occuper des TD, l'autre des CM, ou bien un prof peut s'occuper d'un EC etc.
        public List<Enseignant> enseignants { get; set; }

        public Enseignement()
        {
            this.init();
        }

        public Enseignement(string nom)
        {
            this.init();
            this.nom = nom;
        }

        //On crée un enseignement avec des EC prédéfinis, donc les profs de cet Enseignements sont le/les prof(s) de ces EC
        public Enseignement(string nom, List<EC> ECs)
        {
            this.init();
            this.nom = nom;
            this.ECs = ECs;
            this.updateEnseignants();
        }

        //On crée un enseignement composé d'EC prédéfinis, avec un enseignant qui sera assignés à ceux-ci
        public Enseignement(string nom, List<EC> ECs, Enseignant enseignant)
        {
            this.init();
            this.nom = nom;
            this.ECs = ECs;
            this.assignerEnseignant(enseignant);
        }

        new public void init()
        {
            base.init();
            this.enseignants = new List<Enseignant>();
            this.ECs = new List<EC>();
        }

        //Un enseignant est assigné à l'UE. Cela signifie qu'il prend en charge tous les cours de chaque EC composant cet Enseignement (récursif)
        public void assignerEnseignant(Enseignant enseignant)
        {
            this.enseignants = new List<Enseignant>();
            foreach (EC ec in this.ECs)
            {
                ec.assignerEnseignant(enseignant);
            }
        }

        //Lorsqu'on souhaite voir tous les Enseignants attachés à cet Enseignement (enseignants des cours des ECs de cet Enseignement)
        public void updateEnseignants()
        {
            if (this.ECs.Any())
            {
                foreach (EC ec in this.ECs)
                {
                    //On met à jour les enseignants de chaque EC au cas où il y aurait eu des modifications entre temps
                    ec.updateEnseignants();
                    this.enseignants.AddRange(ec.enseignants);
                }
            }
        }
    }
}