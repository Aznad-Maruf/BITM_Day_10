using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitmDay8.Model;
using BitmDay8.Manager;

namespace BitmDay8
{
    public partial class Item : Form
    {
        public Item()
        {
            InitializeComponent();
        }

        Items items = new Items();

        string verdict;
        DataTable dataTable;


        ItemManager _itemManager = new ItemManager();

        private void AddButton_Click(object sender, EventArgs e)
        {
            GetAllData();
            verdict = _itemManager.CanBeAdded(items);
            if (verdict.Equals("OK"))
            {
                _itemManager.AddToRepository(items);
                MessageBox.Show("Data Saved");
            }
            else
            {
                MessageBox.Show(verdict);
            }

        }

        private void ShowAllButton_Click(object sender, EventArgs e)
        {
            dataTable = _itemManager.ShowAll();
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
            verdict = _itemManager.Update(items);
            if (verdict.Equals("OK"))
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
            verdict = _itemManager.Delete(items);
            if (verdict.Equals("OK"))
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
            dataTable = _itemManager.Search(items);
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
            items.ItemID = itemIdTextBox.Text;
            items.ItemName = itemNameTextBox.Text;
            items.ItemPrice = itemPriceTextBox.Text;
        }

        private void GoToCustomerButton(object sender, EventArgs e)
        {
            this.Hide();
            Customer customer = new Customer();
            customer.Show();
        }

        private void GoToOrder_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerOrder customerOrder = new CustomerOrder();
            customerOrder.Show();
        }
    }
}
