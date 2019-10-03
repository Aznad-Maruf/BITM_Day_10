using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitmDay8.Repository;
using System.Data;
using BitmDay8.Model;

namespace BitmDay8.Manager
{
    public class CustomerOrderManager
    {
        CustomerOrderRepository _customerOrderRepository = new CustomerOrderRepository();

        public string CanBeAdded(CustomerOrders customerOrders)
        {

            return _customerOrderRepository.CanBeAdded(customerOrders);
        }

        public string GetTotalPrice(CustomerOrders customerOrders)
        {

            return _customerOrderRepository.GetTotalPrice(customerOrders);
        }

        public DataTable GetCustomerOrderDataTable()
        {
            return _customerOrderRepository.GetCustomerOrderDataTable();
        }

        public string AddToRepository(CustomerOrders customerOrders)
        {
            return _customerOrderRepository.AddToRepository(customerOrders);
        }

        public DataTable ShowAll()
        {
            return _customerOrderRepository.ShowAll();
        }

        public string Update(CustomerOrders customerOrders)
        {
            return _customerOrderRepository.Update(customerOrders);
        }

        public string Delete(CustomerOrders customerOrders)
        {
            return _customerOrderRepository.Delete(customerOrders);
        }

        public DataTable Search(CustomerOrders customerOrders)
        {
            return _customerOrderRepository.Search(customerOrders);
        }
    }
}
