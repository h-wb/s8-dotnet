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


            if (obj.id == -1)
            {
                obj.id = OutilsSQL.getLastInsertedId("annee", Connexion.getInstance()) + 1;
            }
            
            Annee tc = null;
            tc = this.find(obj.nom);

            if (tc == null)
            {
                using (SqlCommand command_c = new SqlCommand("INSERT INTO annee VALUES (" + obj.id + ", '" + obj.nom + "');", Connexion.getInstance()))
                {
                    command_c.ExecuteNonQuery();
                    // Connexion.getInstance().Close();
                }
            }

            return obj;
        }

        public override void delete(Annee obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM annee WHERE id=" + obj.id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
            //Connexion.getInstance().Close();
        }

        public override Annee find(int id)
        {
            Annee annee = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom FROM annee WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            annee = new Annee(reader_f.GetInt32(0), reader_f.GetString(1));

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

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom FROM annee WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            annee = new Annee(reader_f.GetInt32(0), reader_f.GetString(1));
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
                            ans.Add(new Annee(reader_f.GetInt32(0), reader_f.GetString(1)));
                        }
                    }

                }
            }

            return ans;
        }

        public override Annee update(Annee objAupdate, Annee update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE annee SET nom='" + update.nom + "' WHERE id=" + objAupdate.id + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

            //Connexion.getInstance().Close();
            return objAupdate;
        }
    }
}
