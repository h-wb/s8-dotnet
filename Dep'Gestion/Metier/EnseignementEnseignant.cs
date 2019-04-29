
namespace Metier
{
    public class EnseignementEnseignant : ObjetBase
    {
        private Enseignement _enseignement;
        private Enseignant _enseignant;

        public EnseignementEnseignant()
        {
            this.init();
        }

        public EnseignementEnseignant(int id, Enseignement enseignement, Enseignant enseignant)
        {
            this.init();
            this.Id = id;
            this._enseignement = enseignement;
            this._enseignant = enseignant;
        }

        public EnseignementEnseignant(Enseignement enseignement, Enseignant enseignant)
        {
            this.init();
            this._enseignement = enseignement;
            this._enseignant = enseignant;
        }

        new public void init()
        {
            base.init();
        }

        public Enseignement Enseignement
        {
            get { return _enseignement; }
            set { SetProperty(ref _enseignement, value); }
        }

        public Enseignant Enseignant
        {
            get { return _enseignant; }
            set { SetProperty(ref _enseignant, value); }
        }

    }
}
