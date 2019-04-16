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
    class PartieAnneeSQL : DAO<PartieAnnee>
    {
        public override PartieAnnee create(PartieAnnee obj)
        {
            PartieAnnee tc = null;
            tc = this.find(obj.nom);

            Console.WriteLine(tc);
            // Console.ReadLine();

            if (tc != null)
            {
                return tc;
            }
            else
            {
                obj.id = OutilsSQL.getLastInsertedId("partie_annee", Connexion.getInstance()) + 1;
                using (SqlCommand command_c = new SqlCommand("INSERT INTO partie_annee VALUES (" + obj.id + ", '" + obj.nom + "', '" + obj.heuresATravailler + "');", Connexion.getInstance()))
                {
                    command_c.ExecuteNonQuery();
                    return obj;
                }
            }
        }

        public override void delete(PartieAnnee obj)
        {
            throw new NotImplementedException();
        }

        public override PartieAnnee find(int id)
        {
            throw new NotImplementedException();
        }

        public override PartieAnnee find(string nom)
        {
            throw new NotImplementedException();
        }

        public override PartieAnnee update(PartieAnnee objAupdate, PartieAnnee update)
        {
            throw new NotImplementedException();
        }
    }
}
