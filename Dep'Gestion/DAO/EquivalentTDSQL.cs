using Metier;
using Outils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAO
{
    class EquivalentTDSQL : DAO<EquivalentTD>
    {
        public override EquivalentTD create(EquivalentTD obj)
        {

            Console.WriteLine("id categ enseignant = " + obj.idCategorieEnseignant);
            //Si l'objet n'a pas d'id, on récupère le dernier id dans la table, pour que l'id de l'objet actuel soit le suivant 
            if (obj.id == -1)
            {
                obj.id = OutilsSQL.getLastInsertedId("equivalent_td", Connexion.getInstance()) + 1;
            }

            //On insère une ligne par couple clé/valeur de la table de hachage
            foreach (KeyValuePair<TypeCours, float> entry in obj.ratiosCoursTD)
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
                entry.Key.id = newID;

                /*
                 *id
                 * id_categorie_enseignant
                 * id_type_cours
                 * ratio_cours_TD
                 */
                float ratio = entry.Value;
                string query = @"INSERT INTO equivalent_td(id, id_categorie_enseignant, id_type_cours, ratio_cours_TD)
                    VALUES (" + obj.id
                   + ", " + obj.idCategorieEnseignant
                   + ", " + entry.Key.id
                   + ", " + ratio + ");";

                //Console.WriteLine("id type cours : " + entry.Key.id);
                //Console.ReadLine();

                using (SqlCommand command = new SqlCommand(query, Connexion.getInstance()))
                {
                    command.ExecuteNonQuery();
                    //Console.WriteLine("EQTD crée: " + obj);
                    Connexion.getInstance().Close();
                }

            }

            return obj;
        }

        public override void delete(EquivalentTD obj)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM equivalent_td WHERE id=" + obj.id + ";", Connexion.getInstance()))
            {
                command.ExecuteNonQuery();
                //Connexion.getInstance().Close();
            }
            Connexion.getInstance().Close();
        }

        public override EquivalentTD find(int id)
        {
            //Console.WriteLine("id à trouver dans find:" + id);
            EquivalentTD EQTD = null;

            /*id
            * id_categorie_enseignant
            * id_type_cours
            * ratio_cours_TD
            */
            using (SqlCommand command = new SqlCommand(@"SELECT id, id_categorie_enseignant, id_type_cours, ratio_cours_TD
                FROM equivalent_td WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    Dictionary<TypeCours, float> ratios = new Dictionary<TypeCours, float>();

                    //Factory pour chercher les objets TypeCours dans la DB
                    AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);


                    int idEQTD = -1;
                    int idCategEnseignant = -1;

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            /*id
                            * id_categorie_enseignant
                            * id_type_cours
                            * ratio_cours_TD
                            */
                            /*Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
                            reader.GetString(1));
                            if (reader.IsClosed)
                            {
                                Console.WriteLine("1: closed");
                            }
                            else
                            {
                                Console.WriteLine("1: Open");
                            }*/

                            idEQTD = reader.GetInt32(0);
                            idCategEnseignant = reader.GetInt32(1);
                            int idTypeCours = reader.GetInt32(2);
                            float ratioCoursTD = reader.GetFloat(3);

                            /*
                            if (reader.IsClosed)
                            {
                                Console.WriteLine("2: closed");
                            }
                            else
                            {
                                Console.WriteLine("2: Open");
                            }*/


                            DAO<TypeCours> TPSQL = factoSQL.getTypeCoursDao();
                            ratios.Add(TPSQL.find(idTypeCours), ratioCoursTD);

                            /*
                            if (reader.IsClosed)
                            {
                                Console.WriteLine("3: closed");
                            }
                            else
                            {
                                Console.WriteLine("3: Open");
                            }*/


                            //Console.ReadLine();
                            reader.NextResult();
                        }
                    }
                    else
                    {
                        throw new Exception("Aucun objet avec cet id n'a été trouvé.");
                    }

                    //Construction de l'EQTD grâce à la table de hachage précédemment construite
                    EQTD = new EquivalentTD(ratios);
                    EQTD.idCategorieEnseignant = idCategEnseignant;
                    EQTD.id = idEQTD;

                    reader.Close();
                }



            }


            //Connexion.getInstance().Close();

            return EQTD;
        }

        public override EquivalentTD find(string nom)
        {
            throw new NotImplementedException();
        }

        public override List<EquivalentTD> findAll()
        {
            throw new NotImplementedException();
        }

        public override EquivalentTD update(EquivalentTD obj)
        {
            /*id
            * id_categorie_enseignant
            * id_type_cours
            * ratio_cours_TD
            */

            //Test: supprimer l'objet puis créer le nouveau
            this.delete(obj);

            //Console.WriteLine("obj=" + obj);
            this.create(obj);


            /*SqlCommand command = new SqlCommand("UPDATE equivalent_td SET id_categorie_enseignant='"+obj.nom+"', has_groups="+ConversionFormats.convert(obj.hasGroups)+" WHERE id="+obj.id+";", co);
            command.ExecuteNonQuery();*/

            return obj;
        }
    }
}