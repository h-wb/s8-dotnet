using DAO;
using Metier;
using Outils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dep_Gestion.DAO
{
    class InfosAssignationSQL : DAO<InfosAssignation>
    {
        public override InfosAssignation create(InfosAssignation obj)
        {
            if (obj.Id == -1)
            {
                obj.Id = OutilsSQL.getLastInsertedId("infos_assignation", Connexion.getInstance()) + 1;
            }

            using (SqlCommand command_c = new SqlCommand("INSERT INTO dbo.infos_assignation VALUES (" + obj.Id + ", '" + obj.Nom + "', " + obj.EC.Id + ", " + obj.TypeCours.Id + ", " + obj.Enseignant.Id + ", " + obj.NbHeures.ToString().Replace(",", ".") + ");", Connexion.getInstance()))
            {
                command_c.ExecuteNonQuery();
                // Connexion.getInstance().Close();
            }

            return obj;
        }

        public override void delete(InfosAssignation obj)
        {
            using (SqlCommand command_d = new SqlCommand("DELETE FROM infos_assignation WHERE id=" + obj.Id + ";", Connexion.getInstance()))
            {
                command_d.ExecuteNonQuery();
            }
        }

        public override InfosAssignation find(int id)
        {
            InfosAssignation IA = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_ec, id_typecours, id_enseignant, nb_heures FROM infos_assignation WHERE id=" + id + ";", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<EC> TPSQLEC = factoSQL.getECDAO();
                            DAO<TypeCours> TPSQLTP = factoSQL.getTypeCoursDao();
                            DAO<Enseignant> TPSQLEns = factoSQL.getEnseignantDAO();

                            EC ec = TPSQLEC.find(reader_f.GetInt32(2));
                            TypeCours tp = TPSQLTP.find(reader_f.GetInt32(3));
                            Enseignant ens = TPSQLEns.find(reader_f.GetInt32(4));

                            IA = new InfosAssignation(reader_f.GetInt32(0), reader_f.GetString(1), ec, tp, ens, reader_f.GetDouble(5));

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
            return IA;
        }

        public override InfosAssignation find(string nom)
        {
            InfosAssignation IA = null;

            using (SqlCommand command_f = new SqlCommand("SELECT id, nom, id_ec, id_typecours, id_enseignant, nb_heures FROM infos_assignation WHERE nom='" + nom + "';", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {

                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<EC> TPSQLEC = factoSQL.getECDAO();
                            DAO<TypeCours> TPSQLTP = factoSQL.getTypeCoursDao();
                            DAO<Enseignant> TPSQLEns = factoSQL.getEnseignantDAO();

                            EC ec = TPSQLEC.find(reader_f.GetInt32(2));
                            TypeCours tp = TPSQLTP.find(reader_f.GetInt32(3));
                            Enseignant ens = TPSQLEns.find(reader_f.GetInt32(4));

                            IA = new InfosAssignation(reader_f.GetInt32(0), reader_f.GetString(1), ec, tp, ens, reader_f.GetDouble(5));
                            
                        }
                    }

                    reader_f.Close();
                }
                // Connexion.getInstance().Close();
                return IA;

            }
        }

        public override List<InfosAssignation> findAll()
        {
            List<InfosAssignation> IAs = new List<InfosAssignation>();


            using (SqlCommand command_f = new SqlCommand("SELECT * FROM infos_assignation;", Connexion.getInstance()))
            {
                using (SqlDataReader reader_f = command_f.ExecuteReader())
                {
                    if (reader_f.HasRows)
                    {
                        while (reader_f.Read())
                        {
                            AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);
                            DAO<EC> TPSQLEC = factoSQL.getECDAO();
                            DAO<TypeCours> TPSQLTP = factoSQL.getTypeCoursDao();
                            DAO<Enseignant> TPSQLEns = factoSQL.getEnseignantDAO();

                            EC ec = TPSQLEC.find(reader_f.GetInt32(2));
                            TypeCours tp = TPSQLTP.find(reader_f.GetInt32(3));

                            Enseignant ens = new Enseignant { Nom = "test", Categorie = new Categorie { Nom ="testttt" } };
                            if (!reader_f.IsDBNull(4))
                            {
                                ens = TPSQLEns.find(reader_f.GetInt32(4));
                            }
                                
                            

                            IAs.Add(new InfosAssignation(reader_f.GetInt32(0), reader_f.GetString(1), ec, tp, ens, reader_f.GetDouble(5)));
                        }
                    }

                }
            }

            return IAs;
        }


        public override InfosAssignation update(int idAupdate, InfosAssignation update)
        {
            using (SqlCommand command_u = new SqlCommand(@"UPDATE infos_assignation SET nom='" + update.Nom + "', id_ec=" + update.EC.Id + ", id_typecours= " + update.TypeCours.Id + ", id_enseignant= " + update.Enseignant.Id + ", nb_heures=" + update.NbHeures.ToString().Replace(",", ".") + " WHERE id=" + idAupdate + ";", Connexion.getInstance()))
            {
                command_u.ExecuteNonQuery();
            }

            //Connexion.getInstance().Close();
            return update;
        }
    }
}
