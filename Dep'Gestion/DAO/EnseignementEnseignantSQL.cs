using DAO;
using Metier;
using Outils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dep_Gestion.DAO
{
    class EnseignementEnseignantSQL : DAO<EnseignementEnseignant>
    {
        public override EnseignementEnseignant create(EnseignementEnseignant obj)
        {
            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("enseignement_enseignant", Connexion.getInstance()) + 1;
            }

            using (SqlCommand command_c = new SqlCommand("INSERT INTO enseignement_enseignant VALUES (" + obj.Id + ", " + obj.Enseignement.Id + ", " + obj.Enseignant.Id + ");", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
            }

            return obj;
        }

        public override void delete(EnseignementEnseignant obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM enseignement_enseignant WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
        }

        public override EnseignementEnseignant find(int id)
        {
            EnseignementEnseignant ens = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, id_enseignement, id_enseignant FROM enseignement_enseignant WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Enseignement> TPSQL = factoSQL.getEnseignementDAO();
                            DAO<Enseignant> TPSQL2 = factoSQL.getEnseignantDAO();

                            Enseignement ensfacto = TPSQL.find(reader_f.GetInt32(1));
                            Enseignant ensfacto2 = TPSQL2.find(reader_f.GetInt32(2));

                            ens = new EnseignementEnseignant(reader_f.GetInt32(0), ensfacto, ensfacto2);

                            reader_f.NextResult();
                        }
                    }
                    else
                    {
                        throw new Exception("Aucun objet avec cet id n'a été trouvé.");
                    }

                    reader_f.Close();
                }

            }
            return ens;
        }

        public override EnseignementEnseignant find(string nom)
        {
            throw new NotImplementedException();
        }

        public override List<EnseignementEnseignant> findAll()
        {
            List<EnseignementEnseignant> enss = new List<EnseignementEnseignant>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM enseignement_enseignant;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Enseignement> TPSQL = factoSQL.getEnseignementDAO();
                            DAO<Enseignant> TPSQL2 = factoSQL.getEnseignantDAO();

                            Enseignement ensfacto = TPSQL.find(reader_f.GetInt32(1));
                            Enseignant ensfacto2 = TPSQL2.find(reader_f.GetInt32(2));

                            enss.Add(new EnseignementEnseignant(reader_f.GetInt32(0), ensfacto, ensfacto2));
                        }
                    }

                }
            }

            return enss;
        }

        public override EnseignementEnseignant update(int idAupdate, EnseignementEnseignant update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE enseignement_enseignant SET id_enseignement=" + update.Enseignement.Id + ", id_enseignant=" + update.Enseignant.Id + " WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

            return update;
        }
    }
}
