using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DAO
{
    public class ConnectionSQL
    {
        private static string connectionInformation = System.IO.File.ReadAllText("C:\\Developpement\\DotNet\\dotnet-s8\\DotNet\\Data\\SQL\\SQLconnection.txt");
        static readonly ConnectionSQL instance = new ConnectionSQL();
        private static SqlConnection co = new SqlConnection(connectionInformation);

        public SqlConnection Co
        {
            get
            {
                return co;
            }
            private set
            {
                co = value;
            }
        }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ConnectionSQL()
        {
        }

        ConnectionSQL()
        {
        }

        public static ConnectionSQL Instance
        {
            get
            {
                return instance;
            }
        }
    }
}