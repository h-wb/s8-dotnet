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
    class EnseignementSQL : DAO<Enseignement>
    {
        public override Enseignement create(Enseignement obj)
        {
            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("enseignement", Connexion.getInstance()) + 1;
            }
            
            using (SqlCommand command_c = new SqlCommand("INSERT INTO enseignement VALUES (" + obj.Id + ", '" + obj.Nom + "', " + obj.PartieAnnee.Id + ");", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
            }

            return obj;
        }

        public override void delete(Enseignement obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM enseignement WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
        }

        public override Enseignement find(int id)
        {
            Enseignement ens = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_partie_annee FROM enseignement WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<PartieAnnee> TPSQL = factoSQL.getPartieAnneeDAO();

                            PartieAnnee partannee2 = TPSQL.find(reader_f.GetInt32(2));

                            ens = new Enseignement(reader_f.GetInt32(0), reader_f.GetString(1), partannee2);

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

        public override Enseignement find(string nom)
        {
            Enseignement ens = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_partie_annee FROM enseignement WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<PartieAnnee> TPSQL = factoSQL.getPartieAnneeDAO();

                            PartieAnnee partannee2 = TPSQL.find(reader_f.GetInt32(2));

                            ens = new Enseignement(reader_f.GetInt32(0), reader_f.GetString(1), partannee2);

                            reader_f.NextResult();
                        }
                    }

                    reader_f.Close();
                }

            }
            return ens;
        }

        public override List<Enseignement> findAll()
        {
            List<Enseignement> enses = new List<Enseignement>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM enseignement;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<PartieAnnee> TPSQL = factoSQL.getPartieAnneeDAO();

                            PartieAnnee partannee2 = TPSQL.find(reader_f.GetInt32(2));
                            enses.Add(new Enseignement(reader_f.GetInt32(0), reader_f.GetString(1), partannee2));
                        }
                    }

                }
            }

            return enses;
        }

        public override Enseignement update(int idAupdate, Enseignement update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE enseignement SET nom='" + update.Nom + "', id_partie_annee=" + update.PartieAnnee.Id + " WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

            return update;
        }
    }
}

