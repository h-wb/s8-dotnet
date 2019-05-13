using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Outils
{
    public class OutilsSQL
    {
        public static int getLastInsertedId(string tableName, SqlConnection connection)
        {
            int lastID = -1;
            using (SqlCommand command = new SqlCommand("SELECT TOP(1) id FROM " + tableName + " ORDER BY 1 DESC;", connection))
            //using (SqlCommand command = new SqlCommand("SELECT IDENT_CURRENT('" + tableName+"');", connection))
            {
                command.ExecuteNonQuery();
                lastID = Convert.ToInt32(command.ExecuteScalar());
            }
            return lastID;
        }
    }
}