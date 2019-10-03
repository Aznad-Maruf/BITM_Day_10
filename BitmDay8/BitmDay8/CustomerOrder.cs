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
    public partial class CustomerOrder : Form
    {

        CustomerOrders customerOrders = new CustomerOrders();

        string verdict, selectedRow;
        DataTable dataTable;

        CustomerManager _customerManager = new CustomerManager();
        ItemManager _itemManager = new ItemManager();
        CustomerOrderManager _customerOrderManager = new CustomerOrderManager();



        public CustomerOrder()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            GetAllData();
            verdict = _customerOrderManager.CanBeAdded(customerOrders);
            if (verdict.Equals("OK"))
            {
                _customerOrderManager.AddToRepository(customerOrders);
                totalPriceTextBox.Text = _customerOrderManager.GetTotalPrice(customerOrders);
                customerOrderIDComboBox.DataSource = _customerOrderManager.GetCustomerOrderDataTable();
                MessageBox.Show("Data Saved");
            }
            else
            {
                MessageBox.Show(verdict);
            }

        }

        private void ShowAllButton_Click(object sender, EventArgs e)
        {
            dataTable = _customerOrderManager.ShowAll();
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
            verdict = _customerOrderManager.Update(customerOrders);
            if (verdict.Equals("OK"))
            {
                totalPriceTextBox.Text = _customerOrderManager.GetTotalPrice(customerOrders);
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
            verdict = _customerOrderManager.Delete(customerOrders);
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
            dataTable = _customerOrderManager.Search(customerOrders);
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
            //customerOrders.CustomerOrderID = customerOrderIDComboBox.Text;
            customerOrders.CustomerID = customerComboBox.SelectedValue.ToString();
            customerOrders.ItemID = itemComboBox.SelectedValue.ToString();
            customerOrders.Quantity = quantityComboBox.Text;
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

        private void CustomerOrder_Load(object sender, EventArgs e)
        {
            for (int a_i = 1; a_i <= 20; a_i++) quantityComboBox.Items.Add(a_i);
            customerComboBox.DataSource = _customerManager.ShowAll();
            itemComboBox.DataSource = _itemManager.ShowAll();
            customerOrderIDComboBox.DataSource = _customerOrderManager.GetCustomerOrderDataTable();
        }

        private void DisplayDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.RowIndex.ToString());
            this.selectedRow = e.RowIndex.ToString();
            int selectedRow = Convert.ToInt32(this.selectedRow);
            _customerOrderManager.ShowAll();
            customerOrders.CustomerOrderID = dataTable.Rows[selectedRow]["OrderID"].ToString();
            //_customerOrderManager.Search(customerOrders);

            customerComboBox.SelectedIndex = customerComboBox.FindStringExact(dataTable.Rows[selectedRow]["CustomerName"].ToString());
            itemComboBox.SelectedIndex = itemComboBox.FindStringExact(dataTable.Rows[selectedRow]["ItemName"].ToString());
            quantityComboBox.SelectedIndex = quantityComboBox.FindStringExact(dataTable.Rows[selectedRow]["Quantity"].ToString());
        }
    }
}
