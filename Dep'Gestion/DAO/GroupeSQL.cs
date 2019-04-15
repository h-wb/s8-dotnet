using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.SqlClient;
using Metier;
using Outils;

namespace DAO
{
    public class GroupeSQL : DAO<Groupe>
    {
        public override Groupe create(Groupe obj)
        {
            //Si l'objet n'a pas d'id, on récupère le dernier id dans la table, pour que l'id de l'objet actuel soit le suivant 
            if (obj.id == -1)
            {
                obj.id = OutilsSQL.getLastInsertedId("groupe", Connexion.getInstance()) + 1;
            }

            /*
                *id
                * nom
                * id_enseignant
                * id_cours
                */
            string query = @"INSERT INTO groupe(id, nom, id_enseignant, id_cours)
                VALUES (" + obj.id
                + ", '" + obj.nom
                + "', " + obj.enseignant.id
                + ", " + obj.idCours + ");";

            //Console.ReadLine();

            using (SqlCommand command = new SqlCommand(query, Connexion.getInstance()))
            {
                command.ExecuteNonQuery();
                Connexion.getInstance().Close();
            }
            return obj;
        }

        public override void delete(Groupe obj)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM group WHERE id=" + obj.id + ";", Connexion.getInstance()))
            {
                command.ExecuteNonQuery();
            }
            Connexion.getInstance().Close();
        }

        public override Groupe find(int id)
        {
            //Console.WriteLine("id à trouver dans find:" + id);
            Groupe groupe = null;

            /* id
                * nom
                * id_enseignant
                * id_cours
                */
            using (SqlCommand command = new SqlCommand(@"SELECT nom, id_enseignant, id_cours
                FROM groupe WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    Dictionary<TypeCours, float> ratios = new Dictionary<TypeCours, float>();

                    //Factory pour chercher les objets TypeCours dans la DB
                    AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);


                    int idEnseignant = -1;
                    int idCours = -1;
                    string nom = "";

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            /* 
                              * nom
                              * id_enseignant
                              * id_cours
                              */
                            nom = reader.GetString(0);
                            idEnseignant = reader.GetInt32(1);
                            idCours = reader.GetInt32(2);

                            reader.NextResult();
                        }
                    }
                    else
                    {
                        throw new Exception("Aucun groupe avec cet id n'a été trouvé.");
                    }

                    reader.Close();
                }
            }


            //Connexion.getInstance().Close();

            return groupe;
        }

        public override Groupe find(string nom)
        {
            throw new NotImplementedException();
        }

        public override Groupe update(Groupe obj)
        {
            /*
            * nom
            * id_enseignant
            * id_cours
            */
            SqlCommand command = new SqlCommand(@"UPDATE groupe SET 
            nom='" + obj.nom + "', id_enseignant=" + obj.enseignant.id + ", id_cours = " + obj.idCours
            + " WHERE id=" + obj.id + ";", Connexion.getInstance());
            command.ExecuteNonQuery();

            return obj;
        }
    }
}