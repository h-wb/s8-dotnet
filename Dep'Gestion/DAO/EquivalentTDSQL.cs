﻿using Metier;
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

            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("equivalent_td", Connexion.getInstance()) + 1;
            }


            string query = "INSERT INTO dbo.equivalent_td (id, id_categorie_enseignant, id_type_cours, ratio_cours_td) VALUES (@id, @id_categorie_enseignant, @id_type_cours, @ratio_cours_td)";
            using (SqlCommand command = new SqlCommand(query, Connexion.getInstance()))
            {
                command.Parameters.AddWithValue("@id", obj.Id);
                command.Parameters.AddWithValue("@id_categorie_enseignant", obj.Categorie.Id);
                command.Parameters.AddWithValue("@id_type_cours", obj.TypeCours is null ? DBNull.Value : (object)obj.TypeCours.Id);
                command.Parameters.AddWithValue("@ratio_cours_td", obj.Ratio);

                
                command.ExecuteNonQuery();
            }


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

            using (SqlCommand command_f = new SqlCommand("SELECT id, id_categorie_enseignant, id_type_cours, ratio_cours_td FROM equivalent_td WHERE id=" + id + ";", Connexion.getInstance()))
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

                            Categorie categ = TPSQL.find(reader_f.GetInt32(1));
                            TypeCours tp = TPSQL2.find(reader_f.GetInt32(2));

                            eqtd = new EquivalentTD(reader_f.GetInt32(0), categ, tp, reader_f.GetDouble(3));

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
                            
                            Categorie categ = TPSQL.find(reader_f.GetInt32(1));


                            TypeCours tp = reader_f.IsDBNull(2) ? default(TypeCours) : TPSQL2.find(reader_f.GetInt32(2));

                            eqtds.Add(new EquivalentTD(reader_f.GetInt32(0), categ, tp, reader_f.GetDouble(3)));
                        }
                    }

                }
            }

            return eqtds;
        }

        public override EquivalentTD update(int idAupdate, EquivalentTD obj)
        {
            string query = "UPDATE dbo.equivalent_td SET id = @id, id_categorie_enseignant = @id_categorie_enseignant, id_type_cours = @id_type_cours, ratio_cours_td = @ratio_cours_td WHERE id = @id";
            using (SqlCommand command = new SqlCommand(query, Connexion.getInstance()))
            {
                command.Parameters.AddWithValue("@id", obj.Id);
                command.Parameters.AddWithValue("@id_categorie_enseignant", obj.Categorie.Id);
                command.Parameters.AddWithValue("@id_type_cours", obj.TypeCours is null ? DBNull.Value : (object)obj.TypeCours.Id);
                command.Parameters.AddWithValue("@ratio_cours_td", obj.Ratio);

                command.ExecuteNonQuery();
            }

            return obj;
        }
    }
}