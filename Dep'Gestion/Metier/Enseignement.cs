using DAO;

namespace Metier
{
    public class Enseignement : ObjetBase
    {
        private PartieAnnee _partieAnnee;
        private string _description;

        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
        private static DAO<EnseignementEnseignant> ense = factoSQL.getEnseignementEnseignantDAO();

        

        public Enseignement()
        {
            this.init();
        }

        public Enseignement(int id, string nom, PartieAnnee partieAnnee, string description)
        {
            this.init();
            this.Id = id;
            this.Nom = nom;
            this.PartieAnnee = partieAnnee;
            this._description = description;
        }

        public Enseignement(string nom, PartieAnnee partieAnnee, string description)
        {
            this.init();
            this.Nom = nom;
            this.PartieAnnee = partieAnnee;
            this._description = description;
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

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public void ajouterEnseignant(Enseignant enseignant)
        {
            ense.create(new EnseignementEnseignant(this, enseignant));
        }
    }
}