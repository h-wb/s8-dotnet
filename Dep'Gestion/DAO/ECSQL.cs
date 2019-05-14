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
    class ECSQL : DAO<EC>
    {
        public override EC create(EC obj)
        {
            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("ec", Connexion.getInstance()) + 1;
            }
            
            using (SqlCommand command_c = new SqlCommand("INSERT INTO ec VALUES (" + obj.Id + ", '" + obj.Nom + "', " + obj.Enseignement.Id + ");", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
            }

            return obj;
        }

        public override void delete(EC obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM ec WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
        }

        public override EC find(int id)
        {
            EC ec = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_enseignement FROM ec WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Enseignement> TPSQL = factoSQL.getEnseignementDAO();

                            Enseignement ens = TPSQL.find(reader_f.GetInt32(2));

                            ec = new EC(reader_f.GetInt32(0), reader_f.GetString(1), ens);

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
            return ec;
        }

        public override EC find(string nom)
        {
            EC ec = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_enseignement FROM ec WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Enseignement> TPSQL = factoSQL.getEnseignementDAO();

                            Enseignement ens = TPSQL.find(reader_f.GetInt32(2));

                            ec = new EC(reader_f.GetInt32(0), reader_f.GetString(1), ens);

                            reader_f.NextResult();
                        }
                    }

                    reader_f.Close();
                }

            }
            return ec;
        }

        public override List<EC> findAll()
        {
            List<EC> ECs = new List<EC>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM ec;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Enseignement> TPSQL = factoSQL.getEnseignementDAO();

                            Enseignement ens = TPSQL.find(reader_f.GetInt32(2));
                            ECs.Add(new EC(reader_f.GetInt32(0), reader_f.GetString(1), ens));
                        }
                    }

                }
            }

            return ECs;
        }

        public override EC update(int idAupdate, EC update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE ec SET nom='" + update.Nom + "', id_enseignement=" + update.Enseignement.Id + " WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

            return update;
        }
    }
}
