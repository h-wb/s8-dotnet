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


            if (obj.id == -1)
            {
                obj.id = OutilsSQL.getLastInsertedId("type_cours", Connexion.getInstance()) + 1;
            }

            /*
             * Pour éviter les doublons, on cherche s'il existe déjà un TypeCours avec le même type. 
             * S'il en existe un, on le retourne, sinon, on le crée dans la DB
             */
            Console.WriteLine("création type cours");
            TypeCours tc;
            try
            {
                //Console.WriteLine("try");
                tc = this.find(obj.nom);
            }
            catch (Exception)
            {
                //Console.WriteLine("catch");
                tc = null;
            }

            Console.WriteLine(tc);
            // Console.ReadLine();

            if (tc != null)
            {
                return tc;
            }
            else
            {
                int hasGroups = ConversionFormats.convert(obj.hasGroups);
                using (SqlCommand command_c = new SqlCommand("INSERT INTO type_cours VALUES (" + obj.id + ", '" + obj.nom + "', '" + hasGroups + "');", Connexion.getInstance()))
                {
                    command_c.ExecuteNonQuery();
                    // Connexion.getInstance().Close();
                }



                foreach (Categorie categ in categorie.findAll())
                {
                    int idT = OutilsSQL.getLastInsertedId("equivalent_td", Connexion.getInstance()) + 1;
                    using (SqlCommand command_test = new SqlCommand("INSERT INTO equivalent_td VALUES (" + idT + ", '" + categ.id + "', '" + obj.id + "', 1 );", Connexion.getInstance()))
                    {
                        command_test.ExecuteNonQuery();
                    }
                }
            }

            return obj;


        }

        public override void delete(TypeCours obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM type_cours WHERE id=" + obj.id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
            Connexion.getInstance().Close();
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
                            /*Console.WriteLine("{0}\t{1}", reader_tc.GetInt32(0),
                            reader_tc.GetString(1));*/
                            typeCours = new TypeCours(reader_f.GetInt32(0), reader_f.GetString(1),
                                ConversionFormats.convert(reader_f.GetInt32(2)));
                        }
                    }
                    else
                    {
                        throw new Exception("Aucun objet avec cet id n'a été trouvé.");
                    }

                    reader_f.Close();
                }
                Connexion.getInstance().Close();
                Console.WriteLine("type cours trouvé:" + typeCours);
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
                            /*Console.WriteLine("{0}\t{1}", reader_tc.GetInt32(0),
                            reader_tc.GetString(1));*/
                            typeCours = new TypeCours(reader_f.GetInt32(0), reader_f.GetString(1),
                                ConversionFormats.convert(reader_f.GetInt32(2)));

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
            throw new NotImplementedException();
        }

        public override TypeCours update(TypeCours obj)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE type_cours SET nom='" + obj.nom + "', " +
                "has_groups=" + ConversionFormats.convert(obj.hasGroups) + " WHERE id=" + obj.id + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

            Connexion.getInstance().Close();
            return obj;
        }

    }
}