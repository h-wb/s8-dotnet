﻿using Metier;
using Outils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAO
{
    class TypeCoursSQL : DAO<TypeCours>
    {
        public override TypeCours create(TypeCours obj)
        {

            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
            DAO<Categorie> categorie = factoSQL.getCategorieDAO();


            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("type_cours", Connexion.getInstance()) + 1;
            }

            using (SqlCommand command_c = new SqlCommand("INSERT INTO type_cours VALUES (" + obj.Id + ", '" + obj.Nom + "', '" + obj.Groupes + "');", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
                // Connexion.getInstance().Close();
            }



            //foreach (Categorie categ in categorie.findAll())
            //{
            //    int idT = OutilsSQL.getLastInsertedId("equivalent_td", Connexion.getInstance()) + 1;
            //    using (SqlCommand command_test = new SqlCommand("INSERT INTO equivalent_td VALUES (" + idT + ", '" + categ.Id + "', '" + obj.Id + "', 1 );", Connexion.getInstance()))
            //    {
            //        command_test.ExecuteNonQuery();
            //    }
            //}

            return obj;


        }

        public override void delete(TypeCours obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM type_cours WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
            //Connexion.getInstance().Close();
        }

        public override TypeCours find(string type)
        {
            Console.WriteLine("recherche type cours");
            TypeCours typeCours = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, has_groups FROM type_cours WHERE nom='" + type + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            typeCours = new TypeCours(reader_f.GetInt32(0), reader_f.GetString(1), reader_f.GetInt32(2));
                        }
                    }

                    reader_f.Close();
                }
                //Connexion.getInstance().Close();
                return typeCours;

            }
        }

        public override TypeCours find(int id)
        {
            TypeCours typeCours = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, has_groups FROM type_cours WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            typeCours = new TypeCours(reader_f.GetInt32(0), reader_f.GetString(1),reader_f.GetInt32(2));

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


            //Connexion.getInstance().Close();


            return typeCours;
        }

        public override List<TypeCours> findAll()

        {
            List<TypeCours> tps = new List<TypeCours>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM type_cours;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {

                            tps.Add(new TypeCours(reader_f.GetInt32(0), reader_f.GetString(1), reader_f.GetInt32(2)));
                        }
                    }

                }
            }

            return tps;
        }

        public override TypeCours update(int idAupdate, TypeCours update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE type_cours SET nom='" + update.Nom + "', " +
                "has_groups=" + update.Groupes + " WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

           // Connexion.getInstance().Close();
            return update;
        }
    }
}