﻿using DAO;
using Metier;
using Outils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    class PartieAnneeSQL : DAO<PartieAnnee>
    {
        public override PartieAnnee create(PartieAnnee obj)
        {
            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("partie_annee", Connexion.getInstance()) + 1;
            }
            
            using (SqlCommand command_c = new SqlCommand("INSERT INTO partie_annee VALUES (" + obj.Id + ", '" + obj.Nom + "', " + obj.Annee.Id + ", '" + obj.Description + "');", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
            }

            return obj;
        }

        public override void delete(PartieAnnee obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM partie_annee WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
        }

        public override PartieAnnee find(int id)
        {
            PartieAnnee annee = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_annee, description FROM partie_annee WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Annee> TPSQL = factoSQL.getAnneeDAO();

                            Annee annee2 = TPSQL.find(reader_f.GetInt32(2));
                         
                            annee = new PartieAnnee(reader_f.GetInt32(0), reader_f.GetString(1), annee2, reader_f.GetString(3));

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
            return annee;
        }

        public override PartieAnnee find(string nom)
        {
            PartieAnnee annee = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_annee, description FROM partie_annee WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Annee> TPSQL = factoSQL.getAnneeDAO();

                            Annee annee2 = TPSQL.find(reader_f.GetInt32(2));

                            annee = new PartieAnnee(reader_f.GetInt32(0), reader_f.GetString(1), annee2, reader_f.GetString(3));
                        }
                    }

                    reader_f.Close();
                }
                // Connexion.getInstance().Close();
                return annee;

            }
        }

        public override List<PartieAnnee> findAll()
        {
            List<PartieAnnee> ans = new List<PartieAnnee>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM partie_annee;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<Annee> TPSQL = factoSQL.getAnneeDAO();

                            Annee annee2 = TPSQL.find(reader_f.GetInt32(2));
                            ans.Add(new PartieAnnee(reader_f.GetInt32(0), reader_f.GetString(1), annee2, reader_f.GetString(3)));
                        }
                    }

                }
            }

            return ans;
        }

        public override PartieAnnee update(int idAupdate, PartieAnnee obj)
        {
            string query = "UPDATE dbo.partie_annee SET id = @id, nom = @nom, id_annee = @id_annee, description = @description WHERE id = @id";
            using (SqlCommand command = new SqlCommand(query, Connexion.getInstance()))
            {
                command.Parameters.AddWithValue("@id", obj.Id);
                command.Parameters.AddWithValue("@nom", obj.Nom);
                command.Parameters.AddWithValue("@id_annee", obj.Annee.Id);
                command.Parameters.AddWithValue("@description", obj.Description);

                command.ExecuteNonQuery();
            }

            return obj;



        }
    }
}
