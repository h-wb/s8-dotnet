using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using Outils;
using System.Diagnostics;

namespace DAO
{

    class CategorieSQL : DAO<Categorie>
    {

        public override Categorie create(Categorie obj)
        {

            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
            DAO<Categorie> categorie = factoSQL.getCategorieDAO();
            
            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("categorie_enseignant", Connexion.getInstance()) + 1;
            }

            using (SqlCommand command_c = new SqlCommand(@"INSERT INTO categorie_enseignant VALUES 
                        (" + obj.Id + ", '" + obj.Nom + "', " + obj.heuresATravailler + ");", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
            }
            

            return obj;
        }

        public override void delete(Categorie obj)
        {

            using (SqlCommand command_d = new SqlCommand("DELETE FROM categorie_enseignant WHERE id=" + obj.Id + ";", Connexion.getInstance()))

            {
                command_d.ExecuteNonQuery();
            }

            //Connexion.getInstance().Close();

        }


        public override Categorie find(int id)
        {
            Categorie categorieEnseignant = null;

            

            using (SqlCommand command_f = new SqlCommand("SELECT nom, heures_a_travailler FROM categorie_enseignant WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {

                            categorieEnseignant = new Categorie(id, reader_f.GetString(0), reader_f.GetDouble(1));


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

           // Connexion.getInstance().Close();


            return categorieEnseignant;
        }

        public override Categorie find(string nom)
        {
            Categorie categ = null;


            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, heures_a_travailler FROM categorie_enseignant WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            categ = new Categorie(reader_f.GetInt32(0), reader_f.GetString(1),
                                reader_f.GetDouble(2));
                        }
                    }

                    reader_f.Close();
                }
                //Connexion.getInstance().Close();
                return categ;

            }
        }

        public override List<Categorie> findAll()
        {
            List<Categorie> categories = new List<Categorie>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM categorie_enseignant;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())

                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            categories.Add(new Categorie(reader_f.GetInt32(0), reader_f.GetString(1), reader_f.GetDouble(2)));
                        }
                    }

                }
            }

            return categories;
        }

        public override Categorie update(int idAupdate, Categorie update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE categorie_enseignant SET nom='" + update.Nom + "', " +
               "heures_a_travailler=" + update.heuresATravailler + " WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

           // Connexion.getInstance().Close();
            return update;
        }
        
    }


}