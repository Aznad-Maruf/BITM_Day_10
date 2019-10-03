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
    public class ItemRepository
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private DataTable dataTable;
        private string queryString;
        ConnectionRepository _connectionRepository = new ConnectionRepository();

        public string CanBeAdded(Items items)
        {
            if (String.IsNullOrEmpty(items.ItemName)) return "Name can't be empty!";
            try
            {
                double itemPrice = Convert.ToDouble(items.ItemPrice);
            }
            catch
            {
                return "Price must be valid";
            }
            queryString = "SELECT * FROM Items WHERE ItemName = '" + items.ItemName + "';";
            ConnectAndMakeSql(queryString);

            ExecuteQuery();

            if (NoOfEntries() == 0) return "OK";
            else return "Item Name Must be unique.";

        }

        public string AddToRepository(Items items)
        {
            double itemPrice = Convert.ToDouble(items.ItemPrice);
            queryString = @"INSERT INTO Items ( ItemName, ItemPrice ) VALUES ( '"+items.ItemName+"', "+itemPrice+" );";

            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return "OK";
        }

        public DataTable ShowAll()
        {
            queryString = @"SELECT * FROM Items;";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return dataTable;

        }

        public DataTable Search(Items items)
        {
            queryString = @"SELECT * FROM Items WHERE ItemName = '" + items.ItemName + "';";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return dataTable;
        }

        public string Delete(Items items)
        {
            if (String.IsNullOrEmpty(items.ItemName)) return "Name can't be empty!";
            queryString = @"SELECT * FROM Items WHERE ItemName = '" + items.ItemName + "';";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            if (NoOfEntries() == 0)
            {
                return "Name doesn't exist.";
            }
            else
            {
                queryString = @"DELETE FROM Items WHERE ItemName = '" + items.ItemName + "';";
                ConnectAndMakeSql(queryString);
                PerformNonQuery();
                return "Data Deleted";
            }
        }

        public string Update(Items items)
        {
            if (String.IsNullOrEmpty(items.ItemName)) return "Name can't be empty!";
            queryString = @"SELECT * FROM Items WHERE ItemName = '" + items.ItemName + "';";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();

            if (NoOfEntries() != 0 && dataTable.Rows[0]["ItemID"].ToString() != items.ItemID)
            {
                return "Name Already Exists!";
            }

            int itemID;double itemPrice;
            try
            {
                itemID = Convert.ToInt32(items.ItemID);
            }
            catch
            {
                return "ItemID Must be Valid!";
            }
            try
            {
                itemPrice = Convert.ToDouble(items.ItemPrice);
            }
            catch
            {
                return "ItemPrice Must be Valid!";
            }


            queryString = @"SELECT * FROM Items WHERE ItemID = '" + itemID + "';";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();

            if (NoOfEntries() == 0)
            {
                return "ID doesn't Already Exist!";
            }

            else
            {
                queryString = @"UPDATE Items SET ItemName = '" + items.ItemName + "',ItemPrice = '" + items.ItemPrice + "' WHERE ItemID = " + items.ItemID + ";";
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