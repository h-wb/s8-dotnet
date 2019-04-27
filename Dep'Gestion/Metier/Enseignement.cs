namespace Metier
{
    public class Enseignement : ObjetBase
    {
        private PartieAnnee _partieAnnee;

        public Enseignement()
        {
            this.init();
        }

        public Enseignement(int id, string nom, PartieAnnee partieAnnee)
        {
            this.init();
            this.Id = id;
            this.Nom = nom;
            this.PartieAnnee = partieAnnee;
        }

        public Enseignement(string nom, PartieAnnee partieAnnee)
        {
            this.init();
            this.Nom = nom;
            this.PartieAnnee = partieAnnee;
        }

        new public void init()
        {
            base.init();
        }

        public PartieAnnee PartieAnnee
        {
            get { return _partieAnnee; }
            set { SetProperty(ref _partieAnnee, value); }
        }
    }
}