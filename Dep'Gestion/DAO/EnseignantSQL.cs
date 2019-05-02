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
    class EnseignantSQL : DAO<Enseignant>
    {
        public override Enseignant create(Enseignant obj)
        {
            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("enseignant", Connexion.getInstance()) + 1;
            }
            
            using (SqlCommand command_c = new SqlCommand("INSERT INTO enseignant VALUES (" + obj.Id + ", '" + obj.Nom + "', '" + obj.Prenom + "', " + obj.nbHeuresTravaillees + ", " + obj.Categorie.Id + ");", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
            }

            return obj;
        }

        public override void delete(Enseignant obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM enseignant WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
        }

        public override Enseignant find(int id)
        {
            Enseignant ens = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, prenom, nb_heures_assignees, id_categorie_enseignant FROM enseignant WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Categorie> TPSQL = factoSQL.getCategorieDAO();

                            Categorie categ2 = TPSQL.find(reader_f.GetInt32(4));

                            ens = new Enseignant(reader_f.GetInt32(0), reader_f.GetString(1), reader_f.GetString(2), reader_f.GetDouble(3), categ2);

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
            return ens;
        }

        public override Enseignant find(string nom)
        {
            Enseignant ens = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, prenom, nb_heures_assignees, id_categorie_enseignant FROM enseignant WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Categorie> TPSQL = factoSQL.getCategorieDAO();

                            Categorie categ2 = TPSQL.find(reader_f.GetInt32(4));

                            ens = new Enseignant(reader_f.GetInt32(0), reader_f.GetString(1), reader_f.GetString(2), reader_f.GetDouble(3), categ2);

                            reader_f.NextResult();
                        }
                    }

                    reader_f.Close();
                }

            }
            return ens;
        }

        public override List<Enseignant> findAll()
        {
            List<Enseignant> enss = new List<Enseignant>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM enseignant;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Categorie> TPSQL = factoSQL.getCategorieDAO();

                            Categorie categ2 = TPSQL.find(reader_f.GetInt32(4));

                            enss.Add(new Enseignant(reader_f.GetInt32(0), reader_f.GetString(1), reader_f.GetString(2), reader_f.GetDouble(3), categ2));
                        }
                    }

                }
            }

            return enss;
        }

        public override Enseignant update(int idAupdate, Enseignant update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE enseignant SET nom='" + update.Nom + "', prenom='" + update.Prenom + "', nb_heures_assignees=" + update.nbHeuresTravaillees + ", id_categorie_enseignant=" + update.Categorie.Id + " WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

            return update;
        }
    }
}
