using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using BitmDay8.Model;

namespace BitmDay8.Repository
{
    public class CustomerRepository
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private DataTable dataTable;
        private string queryString;
        ConnectionRepository _connectionRepository = new ConnectionRepository();

        public string CanBeAdded(Customers customers)
        {
            if (String.IsNullOrEmpty(customers.CustomerName)) return "Name can't be empty!";
            queryString = "SELECT * FROM Customers WHERE CustomerName = '" + customers.CustomerName + "';";
            ConnectAndMakeSql(queryString);

            ExecuteQuery();

            if (NoOfEntries() == 0) return "OK";
            else return "Customer Name Must be unique.";

        }

        public string AddToRepository(Customers customers)
        {
            queryString = @"INSERT INTO Customers ( CustomerName, ContactNo, Address ) VALUES ( '"+customers.CustomerName+"', '"+customers.ContactNo+"', '"+customers.Address+"' );";

            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return "OK";
        }

        public DataTable ShowAll()
        {
            queryString = @"SELECT * FROM Customers;";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return dataTable;

        }

        public DataTable Search(Customers customers)
        {
            queryString = @"SELECT * FROM Customers WHERE CustomerName = '" + customers.CustomerName + "';";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return dataTable;
        }

        public string Delete(Customers customers)
        {
            if (String.IsNullOrEmpty(customers.CustomerName)) return "Name can't be empty!";
            queryString = @"SELECT * FROM Customers WHERE CustomerName = '" + customers.CustomerName + "';";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            if(NoOfEntries() == 0)
            {
                return "Name doesn't exist.";
            }
            else
            {
                queryString = @"DELETE FROM Customers WHERE CustomerName = '" + customers.CustomerName + "';";
                ConnectAndMakeSql(queryString);
                PerformNonQuery();
                return "Data Deleted";
            }
        }

        public string Update(Customers customers)
        {
            if (String.IsNullOrEmpty(customers.CustomerName)) return "Name can't be empty!";
            queryString = @"SELECT * FROM Customers WHERE CustomerName = '" + customers.CustomerName + "';";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            
            if (NoOfEntries() != 0 && dataTable.Rows[0]["CustomerID"].ToString() != customers.CustomerID)
            {
                return "Name Already Exists!";
            }

            int customerID;
            try
            {
                customerID = Convert.ToInt32(customers.CustomerID);
            }
            catch
            {
                return "CustomerID Must be Valid!";
            }


            queryString = @"SELECT * FROM Customers WHERE CustomerID = '" + customerID + "';";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();

            if (NoOfEntries() == 0)
            {
                return "ID doesn't Already Exist!";
            }

            else
            {
                queryString = @"UPDATE Customers SET CustomerName = '" + customers.CustomerName + "',ContactNo = '" + customers.ContactNo + "', Address = '" + customers.Address + "' WHERE CustomerID = "+customers.CustomerID+";";
                ConnectAndMakeSql(queryString);
                PerformNonQuery();
                return "Data Updated";
            }
            
        }


        // Methods ------------o------------

        private void ConnectAndMakeSql(string queryString)
        {
            sqlConnection = _connectionRepository.ConnectToDatabase();

            sqlCommand = _connectionRepository.CreateSqlCommand(sqlConnection, queryString);

        }

        private void ExecuteQuery()
        {
            dataTable = _connectionRepository.ExecuteQuery(sqlConnection, sqlCommand);
        }

        private int NoOfEntries()
        {
            return dataTable.Rows.Count;
            
        }

        private void PerformNonQuery()
        {
            _connectionRepository.PerformNonQuery(sqlConnection, sqlCommand);
        }

    }
}
