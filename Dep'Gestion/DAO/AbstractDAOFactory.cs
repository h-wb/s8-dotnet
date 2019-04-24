using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;

namespace DAO
{

    public abstract class AbstractDAOFactory
    {
        public abstract DAO<EquivalentTD> getEquivalentTDDao();
        public abstract DAO<Groupe> getGroupeDAO();
        public abstract DAO<TypeCours> getTypeCoursDao();
        public abstract DAO<Categorie> getCategorieDAO();
        public abstract DAO<Annee> getAnneeDAO();
        public abstract DAO<PartieAnnee> getPartieAnneeDAO();
        public abstract DAO<Enseignement> getEnseignementDAO();
        public abstract DAO<EC> getECDAO();

        public static AbstractDAOFactory getFactory(types type)
        {
            if (type.Equals(types.SQL_FACTORY))
            {
                return new SQLFactory();
            }
            return null;

        }
    }
}