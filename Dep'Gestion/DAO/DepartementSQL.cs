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
    class DepartementSQL : DAO<Departement>
    {
        public override Departement create(Departement obj)
        {
            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("departement", Connexion.getInstance()) + 1;
            }
            
            using (SqlCommand command_c = new SqlCommand("INSERT INTO departement VALUES (" + obj.Id + ", '" + obj.Nom + "');", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
            }

            return obj;
        }

        public override void delete(Departement obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM departement WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
        }

        public override Departement find(int id)
        {
            Departement dep = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom FROM departement WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            dep = new Departement(reader_f.GetInt32(0), reader_f.GetString(1));

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
            return dep;
        }

        public override Departement find(string nom)
        {
            Departement dep = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom FROM departement WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            dep = new Departement(reader_f.GetInt32(0), reader_f.GetString(1));

                            reader_f.NextResult();
                        }
                    }

                    reader_f.Close();
                }

            }
            return dep;
        }

        public override List<Departement> findAll()
        {
            List<Departement> deps = new List<Departement>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM departement;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            deps.Add(new Departement(reader_f.GetInt32(0), reader_f.GetString(1)));
                        }
                    }

                }
            }

            return deps;
        }

        public override Departement update(int idAupdate, Departement update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE departement SET nom='" + update.Nom + "' WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }
            
            return update;
        }
    }
}
