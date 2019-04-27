using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class EC : ObjetBase
    {
        public Enseignement enseignement { get; set; }

        public EC(int id, string nom, Enseignement enseignement)
        {
            this.init();
            this.Id = id;
            this.Nom = nom;
            this.enseignement = enseignement;
        }

        public EC(string nom, Enseignement enseignement)
        {
            this.init();
            this.Nom = nom;
            this.enseignement = enseignement;
        }

        new public void init()
        {
            base.init();
        }
    }
}