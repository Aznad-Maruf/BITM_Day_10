using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitmDay8.Manager;
using BitmDay8.Model;

namespace BitmDay8
{
    public partial class Customer : Form
    {

        Customers customers = new Customers();
        
        string verdict;
        DataTable dataTable;
        

        CustomerManager _customerManager = new CustomerManager();

        

        public Customer()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            GetAllData();
            verdict = _customerManager.CanBeAdded(customers);
            if( verdict.Equals("OK"))
            {
                _customerManager.AddToRepository(customers);
                MessageBox.Show("Data Saved");
            }
            else
            {
                MessageBox.Show(verdict);
            }
            
        }

        private void ShowAllButton_Click(object sender, EventArgs e)
        {
            dataTable = _customerManager.ShowAll();
            if (dataTable.Rows.Count != 0)
            {
                displayDataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("No Data Exists");
            }
        }


        private void UpdateButton_Click(object sender, EventArgs e)
        {
            GetAllData();
            verdict = _customerManager.Update(customers);
            if( verdict.Equals("OK"))
            {
                MessageBox.Show("Data Updated");
            }
            else
            {
                MessageBox.Show(verdict);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            GetAllData();
            verdict = _customerManager.Delete(customers);
            if(verdict.Equals("OK"))
            {
                MessageBox.Show("Data Deleted");
            }
            else
            {
                MessageBox.Show(verdict);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            GetAllData();
            dataTable = _customerManager.Search(customers);
            if (dataTable.Rows.Count != 0)
            {
                displayDataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("No Data Found!");
            }
        }

        // Methods ------------------o----------

        private void GetAllData()
        {
            customers.CustomerID = customerIdTextBox.Text;
            customers.CustomerName = customerNameTextBox.Text;
            customers.ContactNo = customerContactNoTextBox.Text;
            customers.Address = customerAddressTextBox.Text;
        }

        private void GoToItemButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Item item = new Item();
            item.Show();
        }

        private void GoToOrderButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerOrder customerOrder = new CustomerOrder();
            customerOrder.Show();
        }
    }
}
