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
    public class CustomerOrderRepository
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private DataTable dataTable;
        private string queryString;
        private int quantity;
        private int customerID;
        private int itemID;
        ConnectionRepository _connectionRepository = new ConnectionRepository();

        public string CanBeAdded(CustomerOrders customerOrders)
        {
            try
            {
                quantity = Convert.ToInt32(customerOrders.Quantity);
                customerID = Convert.ToInt32(customerOrders.CustomerID);
                itemID = Convert.ToInt32(customerOrders.ItemID);
            }
            catch
            {
                return "Quantity Must be Valid!";
            }
            return "OK";

        }

        public string AddToRepository(CustomerOrders customerOrders)
        {
            queryString = @"INSERT INTO CustomerOrders ( CustomerID, ItemID, Quantity ) VALUES (" +customerID +", "+itemID+", "+quantity+" );";

            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return "OK";
        }

        public string GetTotalPrice(CustomerOrders customerOrders)
        {
            quantity = Convert.ToInt32(customerOrders.Quantity);
            itemID = Convert.ToInt32(customerOrders.ItemID);
            queryString = @"SELECT ItemPrice FROM Items WHERE ItemID = "+itemID+"";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return (Convert.ToDouble( dataTable.Rows[0]["ItemPrice"].ToString())*quantity).ToString();
        }

        public DataTable GetSelectedRow( CustomerOrders customerOrders)
        {
            queryString = @"SELECT CustomerOrderID AS OrderID, CustomerName, ItemName, Quantity, Quantity*ItemPrice AS TotalPrice FROM (( CustomerOrders INNER JOIN Customers ON CustomerOrders.CustomerID =  AND Customers.CustomerID= 1011 ) LEFT JOIN Items ON CustomerOrders.ItemID = Items.ItemID);";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return dataTable;
        }

        public DataTable GetCustomerOrderDataTable()
        {
            queryString = @"SELECT CustomerOrderID FROM CustomerOrders";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return dataTable;
        }

        public DataTable ShowAll()
        {
            queryString = @"SELECT CustomerOrderID AS OrderID, CustomerName, ItemName, Quantity,                        Quantity*ItemPrice AS TotalPrice 
                            FROM (( CustomerOrders
                            LEFT JOIN Customers ON CustomerOrders.CustomerID = Customers.CustomerID )
                            LEFT JOIN Items ON CustomerOrders.ItemID = Items.ItemID);";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return dataTable;

        }

        public DataTable Search(CustomerOrders customerOrders)
        {
            customerID = Convert.ToInt32(customerOrders.CustomerID);
            queryString = @"SELECT CustomerOrderID AS OrderID, CustomerName, ItemName, Quantity, Quantity*ItemPrice AS TotalPrice FROM (( CustomerOrders INNER JOIN Customers ON CustomerOrders.CustomerID = "+customerID+" AND Customers.CustomerID= "+customerID+" ) LEFT JOIN Items ON CustomerOrders.ItemID = Items.ItemID);";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            return dataTable;
        }

        public string Delete(CustomerOrders customerOrders)
        {
            if (String.IsNullOrEmpty(customerOrders.CustomerOrderID)) return "Name can't be empty!";

            queryString = @"DELETE FROM CustomerOrders WHERE CustomerOrderID = "+ Convert.ToInt32(customerOrders.CustomerOrderID)+";";
            ConnectAndMakeSql(queryString);
            PerformNonQuery();
            return "Data Deleted";
        }

        public string Update(CustomerOrders customerOrders)
        {
            if (String.IsNullOrEmpty(customerOrders.CustomerOrderID)) return "Select a order";
            if (String.IsNullOrEmpty(customerOrders.Quantity)) return "Select Quantity";

            string customerName, itemName;

            customerID = Convert.ToInt32(customerOrders.CustomerID);
            itemID = Convert.ToInt32(customerOrders.ItemID);

            queryString = @"Select CustomerName from Customers WHERE CustomerID = "+customerID+" ";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            customerName = dataTable.Rows[0]["CustomerName"].ToString();

            queryString = @"Select ItemName from Items WHERE ItemID = " + itemID + " ";
            ConnectAndMakeSql(queryString);
            ExecuteQuery();
            itemName = dataTable.Rows[0]["ItemName"].ToString();

            queryString = @"UPDATE CustomerOrders SET CustomerID= '" + customerID + "',ItemID = '" + customerOrders.ItemID + "', Quantity = '" + customerOrders.Quantity + "' WHERE CustomerOrderID = " + customerOrders.CustomerOrderID + ";";
            ConnectAndMakeSql(queryString);
            PerformNonQuery();
            return "OK";

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
