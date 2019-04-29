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
    class AnneeSQL : DAO<Annee>
    {
        public override Annee create(Annee obj)
        {


            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("annee", Connexion.getInstance()) + 1;
            }
            

            using (SqlCommand command_c = new SqlCommand("INSERT INTO annee VALUES (" + obj.Id + ", '" + obj.Nom + "', " + obj._departement.Id + ", '" + obj._description + "');", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
                // Connexion.getInstance().Close();
            }

            return obj;
        }

        public override void delete(Annee obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM annee WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
            //Connexion.getInstance().Close();
        }

        public override Annee find(int id)
        {
            Annee annee = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_departement, description FROM annee WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Departement> TPSQL = factoSQL.getDepartementDAO();

                            Departement dep2 = TPSQL.find(reader_f.GetInt32(2));

                            annee = new Annee(reader_f.GetInt32(0), reader_f.GetString(1), dep2, reader_f.GetString(3));

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
            return annee;
        }

        public override Annee find(string nom)
        {
            Annee annee = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_departement, description FROM annee WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {

                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Departement> TPSQL = factoSQL.getDepartementDAO();

                            Departement dep2 = TPSQL.find(reader_f.GetInt32(2));

                            annee = new Annee(reader_f.GetInt32(0), reader_f.GetString(1), dep2, reader_f.GetString(3));
                        }
                    }

                    reader_f.Close();
                }
               // Connexion.getInstance().Close();
                return annee;

            }
        }

        public override List<Annee> findAll()
        {
            List<Annee> ans = new List<Annee>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM annee;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Departement> TPSQL = factoSQL.getDepartementDAO();

                            Departement dep2 = TPSQL.find(reader_f.GetInt32(2));
                            
                            ans.Add(new Annee(reader_f.GetInt32(0), reader_f.GetString(1), dep2, reader_f.GetString(3)));
                        }
                    }

                }
            }

            return ans;
        }

        public override Annee update(int idAupdate, Annee update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE annee SET nom='" + update.Nom + "', id_departement=" + update._departement.Id + ", description= '" + update._description + "' WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

            //Connexion.getInstance().Close();
            return update;
        }
    }
}
