using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BitmDay8.Repository
{

    public class ConnectionRepository
    {
        

        public SqlConnection ConnectToDatabase()
        {
            string connectionString = @"Server=ROBINHOOD\MARUFROBINSQL; Database=CoffeeShop; Integrated Security=True";
            return new SqlConnection(connectionString);
        }

        public SqlCommand CreateSqlCommand(SqlConnection sqlConnection, string commandString)
        {
            return new SqlCommand(commandString,sqlConnection);
        }

        public void PerformNonQuery( SqlConnection sqlConnection, SqlCommand sqlCommand)
        {
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public DataTable ExecuteQuery( SqlConnection sqlConnection, SqlCommand sqlCommand)
        {
            sqlConnection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            return dataTable;
        }
    }


}
