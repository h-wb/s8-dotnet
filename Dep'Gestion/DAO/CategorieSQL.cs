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

namespace DAO
{

    class CategorieSQL : DAO<Categorie>
    {

        public override Categorie create(Categorie obj)
        {
            //Attribution de l'id à l'objet ainsi qu'à l'idCategorieEnseignant de son attribut EQTD
            obj.id = OutilsSQL.getLastInsertedId("categorie_enseignant", Connexion.getInstance()) + 1;

            //Factory EquivalentTD
            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);



            //On teste si l'equivalentTD existe dans la DB et on le crée sinon
   

            //obj.EQTD.idCategorieEnseignant = obj.id;

            //id, id_eqtd, heures_a_travailler

            using (SqlCommand command_c = new SqlCommand(@"INSERT INTO categorie_enseignant VALUES 
                            (" + obj.id + ", '" + obj.nom + ", " + obj.heuresATravailler + ");", Connexion.getInstance()))
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
            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
            DAO<EquivalentTD> EQTDSQL = factoSQL.getEquivalentTDDao();
            EQTDSQL.delete(obj.EQTD);

            using (SqlCommand command = new SqlCommand("DELETE FROM categorie_enseignant WHERE id=" + obj.id + ";", Connexion.getInstance()))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Categorie enseignant supprimée");
            }

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

            int idEQTD = -1;
            double heuresATravailler = -1;
            string nom = "";

            using (SqlCommand command_f = new SqlCommand("SELECT nom, id_eqtd, heures_a_travailler FROM categorie_enseignant WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            nom = reader_f.GetString(0);
                            idEQTD = reader_f.GetInt32(1);
                            heuresATravailler = reader_f.GetDouble(2);

                            //On récupère l'objet EquivalentTD associé à l'id_eqtd
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<EquivalentTD> EQTDSQL = factoSQL.getEquivalentTDDao();
                            EquivalentTD eqTD = EQTDSQL.find(idEQTD);

                            //Création de l'objet Categorie maintenant qu'on a tous ses attributs
                            categorieEnseignant = new Categorie(nom, heuresATravailler, eqTD);

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
                AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                DAO<EquivalentTD> EQTDSQL = factoSQL.getEquivalentTDDao();

                EquivalentTD eqTD = null;
                try
                {
                    eqTD = EQTDSQL.find(obj.EQTD.id);
                    //L'EquivalentTD existe dans la DB
                    //Mise à jour de celui-ci, si l'objet dans la DB est différent de l'attribut de obj
                    if (!eqTD.Equals(obj.EQTD))
                    {
                        EQTDSQL.update(obj.EQTD);
                    }

                }
                catch (Exception)
                {
                    //L'EquivalentTD est nouveau donc on le crée dans la DB
                    eqTD = EQTDSQL.create(obj.EQTD);
                }



                //Maintenant que l'EquivalentTD a été pris en charge, on peut mettre à jour la table categorie_enseignant sans difficulté
                using (SqlCommand command_u = new SqlCommand(@"UPDATE categorie_enseignant SET nom='" + obj.nom + "', " +
                    "heures_a_travailler=" + obj.heuresATravailler + ", " +
                    "id_eqtd=" + obj.EQTD.id + " WHERE id=" + obj.id + ";", Connexion.getInstance()))
                {
                    command_u.ExecuteNonQuery();
                }

                Connexion.getInstance().Close();
            }

            return obj;

        }



    }
}