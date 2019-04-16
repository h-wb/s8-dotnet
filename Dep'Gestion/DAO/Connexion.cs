using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace DAO
{
    public class Connexion
    {
        //private static String chaineConnexion = System.IO.File.ReadAllText("..\\..\\..\\..\\Data\\SQL\\SQLconnection.txt");
        private static String chaineConnexion = @"Data Source=DESKTOP-6EB7DT5;Initial Catalog=ProjetDotnet;Integrated Security=SSPI";

        private static SqlConnection connexion { get; set; }

        public Connexion()
        {
            creerConnexion();
        }

        public static SqlConnection getInstance()
        {
            if (connexion == null || connexion.State == ConnectionState.Closed)
            {
                new Connexion();
            }
            return connexion;
        }

        public SqlConnection creerConnexion()
        {
            //System.Console.WriteLine(chaineConnexion);
            connexion = new SqlConnection(chaineConnexion);
            connexion.Open();
            return connexion;
        }

    }
}