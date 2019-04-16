using DAO;
using Metier;
using Outils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dep_Gestion.DAO
{
    class EnseignementSQL : DAO<Enseignement>
    {
        public override Enseignement create(Enseignement obj)
        {
            Enseignement tc = null;
            tc = this.find(obj.nom);

            Console.WriteLine(tc);
            // Console.ReadLine();

            if (tc != null)
            {
                return tc;
            }
            else
            {
                obj.id = OutilsSQL.getLastInsertedId("enseignement", Connexion.getInstance()) + 1;
                using (SqlCommand command_c = new SqlCommand("INSERT INTO enseignement VALUES (" + obj.id + ", '" + obj.nom + "');", Connexion.getInstance()))
                {
                    command_c.ExecuteNonQuery();
                    return obj;
                }
            }
        }

        public override void delete(Enseignement obj)
        {
            Debug.WriteLine(obj.id);
            using (SqlCommand command = new SqlCommand("DELETE FROM enseignement WHERE id=" + obj.id + ";", Connexion.getInstance()))
            {
                command.ExecuteNonQuery();
            }


            Connexion.getInstance().Close();
            Console.ReadLine();
        }

        public override Enseignement find(int id)
        {
            Enseignement ens = null;
            
            string nom = "";

            using (SqlCommand command_f = new SqlCommand("SELECT nom FROM enseignement WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            nom = reader_f.GetString(0);
                            ens = new Enseignement(id, nom);

                        }
                    }
                    else
                    {
                        throw new Exception("Aucun objet avec cet id n'a été trouvé.");
                    }

                    reader_f.Close();
                }

            }

            Connexion.getInstance().Close();


            return ens;
        }

        public override Enseignement find(string nom)
        {
            Enseignement ens = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom FROM enseignement WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            ens = new Enseignement(reader_f.GetInt32(0), reader_f.GetString(1));
                        }
                    }

                    reader_f.Close();
                }
                Connexion.getInstance().Close();
                return ens;

            }
        }

        public override Enseignement update(Enseignement objAupdate, Enseignement update)
        {
            
           using (SqlCommand command_u = new SqlCommand(@"UPDATE enseignement SET nom='" + update.nom + "' WHERE id=" + objAupdate.id + ";", Connexion.getInstance()))
           {
                command_u.ExecuteNonQuery();
           }

           Connexion.getInstance().Close();

            return update;
        }
    }
}
