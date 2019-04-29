using Metier;
using Outils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DAO
{
    class EquivalentTDSQL : DAO<EquivalentTD>
    {
        public override EquivalentTD create(EquivalentTD obj)
        {

            /*Console.WriteLine("id categ enseignant = " + obj.idCategorieEnseignant);
            //Si l'objet n'a pas d'id, on récupère le dernier id dans la table, pour que l'id de l'objet actuel soit le suivant 
            if (obj.id == -1)
            {
                obj.id = OutilsSQL.getLastInsertedId("equivalent_td", Connexion.getInstance()) + 1;
            }

            //On insère une ligne par couple clé/valeur de la table de hachage
            foreach (KeyValuePair<TypeCours, double> entry in obj.ratiosCoursTD)
            {
                AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                DAO<TypeCours> TPSQL = factoSQL.getTypeCoursDao();
                //Il faut vérifier que les types de cours existent dans la DB. S'ils n'existent pas, on les crée
                int newID = -1;
                try
                {
                    newID = TPSQL.find(entry.Key.nom).id;
                }
                catch (Exception)
                {
                    Console.WriteLine("type cours inexistant dans la DB: création de celui-ci");

                    newID = TPSQL.create(entry.Key).id;
                }
                entry.Key.id = newID;*/

            /*
             *id
             * id_categorie_enseignant
             * id_type_cours
             * ratio_cours_TD
             */
            /*double ratio = entry.Value;
            string query = @"INSERT INTO equivalent_td(id, id_categorie_enseignant, id_type_cours, ratio_cours_TD)
                VALUES (" + obj.id
               + ", " + obj.idCategorieEnseignant
               + ", " + entry.Key.id
               + ", " + ratio + ");";
               */
            //Console.WriteLine("id type cours : " + entry.Key.id);
            //Console.ReadLine();

            /*using (SqlCommand command = new SqlCommand(query, Connexion.getInstance()))
            {
                command.ExecuteNonQuery();
                //Console.WriteLine("EQTD crée: " + obj);
                Connexion.getInstance().Close();
            }

        }*/
            return obj;
        }

        public override void delete(EquivalentTD obj)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM equivalent_td WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command.ExecuteNonQuery();
            }
           // Connexion.getInstance().Close();
        }

        public override EquivalentTD find(int id)
        {
            EquivalentTD eqtd = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id_categorie_enseignant, id_type_cours, ratio_cours_td FROM equivalent_td WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Categorie> TPSQL = factoSQL.getCategorieDAO();
                            DAO<TypeCours> TPSQL2 = factoSQL.getTypeCoursDao();

                            Categorie categ = TPSQL.find(reader_f.GetInt32(0));
                            TypeCours tp = TPSQL2.find(reader_f.GetInt32(1));

                            eqtd = new EquivalentTD(id, categ, tp, reader_f.GetDouble(2));

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


            return eqtd;

        }

        public override EquivalentTD find(string nom)
        {
            throw new NotImplementedException();
        }

        public override List<EquivalentTD> findAll()
        {
            List<EquivalentTD> eqtds = new List<EquivalentTD>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM equivalent_td;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Categorie> TPSQL = factoSQL.getCategorieDAO();
                            DAO<TypeCours> TPSQL2 = factoSQL.getTypeCoursDao();
                            Debug.WriteLine(reader_f.GetInt32(0));
                            Debug.WriteLine(reader_f.GetInt32(1));
                            Categorie categ = TPSQL.find(reader_f.GetInt32(0));
                            TypeCours tp = TPSQL2.find(reader_f.GetInt32(1));

                            eqtds.Add(new EquivalentTD(reader_f.GetInt32(0), categ, tp, reader_f.GetDouble(3)));
                        }
                    }

                }
            }

            return eqtds;
        }

        public override EquivalentTD update(int idAupdate, EquivalentTD update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE equivalent_td SET id_categorie_enseignant=" + update.categ.Id + ", " +
               "id_type_cours=" + update.TypeCours.Id + ", ratio_cours_td=" + update.ratio + " WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

            //Connexion.getInstance().Close();
            return update;
        }
    }
}