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
            //Attribution de l'id à l'objet ainsi qu'à l'idCategorieEnseignant de son attribut EQTD
            obj.id = OutilsSQL.getLastInsertedId("categorie_enseignant", Connexion.getInstance()) + 1;

            //Factory EquivalentTD

            Debug.WriteLine(obj.heuresATravailler);


            //On teste si l'equivalentTD existe dans la DB et on le crée sinon


            //obj.EQTD.idCategorieEnseignant = obj.id;

            //id, id_eqtd, heures_a_travailler
     

            using (SqlCommand command_c = new SqlCommand("INSERT INTO categorie_enseignant VALUES (" + obj.id + ", '" + obj.nom + "', '" + obj.heuresATravailler + "');", Connexion.getInstance()))
            {
                //obj.EQTD.idCategorieEnseignant = currentID;
                command_c.ExecuteNonQuery();
                Console.WriteLine("categorie créée");
                return obj;
            }
        }

        public override void delete(Categorie obj)
        {
            //il faut aussi détruire aussi l'equivalentTD associé



            using (SqlCommand command = new SqlCommand("DELETE FROM categorie_enseignant WHERE id=" + obj.id + ";", Connexion.getInstance()))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Categorie enseignant supprimée");
            }

  
            Connexion.getInstance().Close();
            Console.ReadLine();
        }


        public override Categorie find(int id)
        {
            Categorie categorieEnseignant = null;

            //int idEQTD = -1;
            double heuresATravailler = -1;
            string nom = "";

            using (SqlCommand command_f = new SqlCommand("SELECT nom, heures_a_travailler FROM categorie_enseignant WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            nom = reader_f.GetString(0);
                            heuresATravailler = reader_f.GetDouble(1);

                            categorieEnseignant = new Categorie(id, nom, heuresATravailler);

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


            return categorieEnseignant;
        }

        public override Categorie find(string nom)
        {
            throw new NotImplementedException();
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
                            Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            Debug.WriteLine(reader_f.GetInt32(0));
                            Debug.WriteLine(reader_f.GetString(1));
                            Debug.WriteLine(reader_f.GetDouble(2));
                            Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            categories.Add(new Categorie(reader_f.GetInt32(0), reader_f.GetString(1), reader_f.GetDouble(2)));
                        }
                    }

                }
            }

            return categories;

        }


        public override Categorie update(Categorie obj)
        {
            //Si l'objet à un ID de -1, c'est qu'il n'existe pas encore dans la DB, auquel cas on le crée, tout simplement
            if (obj.id == -1)
            {
                obj = this.create(obj);
            }
            else
            {
                //Dans toute la suite de l'algo l'objet existe déjà dans la DB:


                //On met à jour l'EquivalentTD si des changements ont été effectués dessus ou s'il n'a pas été crée dans la DB
     

                //Maintenant que l'EquivalentTD a été pris en charge, on peut mettre à jour la table categorie_enseignant sans difficulté
                using (SqlCommand command_u = new SqlCommand(@"UPDATE categorie_enseignant SET nom='" + obj.nom + "', " +
                    "heures_a_travailler=" + obj.heuresATravailler + ", " + " WHERE id=" + obj.id + ";", Connexion.getInstance()))
                {
                    command_u.ExecuteNonQuery();
                }

                Connexion.getInstance().Close();
            }

            return obj;

        }



    }
}