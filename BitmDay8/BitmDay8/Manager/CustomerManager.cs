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
    public class CustomerManager
    {
        CustomerRepository _customerRepository = new CustomerRepository();

        public string CanBeAdded(Customers customers)
        {

            return _customerRepository.CanBeAdded(customers);
        }

        public string AddToRepository(Customers customers)
        {
            return _customerRepository.AddToRepository(customers);
        }

        public DataTable ShowAll()
        {
            return _customerRepository.ShowAll();
        }

        public string Update(Customers customers)
        {
            return _customerRepository.Update(customers);
        }

        public string Delete(Customers customers)
        {
            return _customerRepository.Delete(customers);
        }

        public DataTable Search(Customers customers)
        {
            return _customerRepository.Search(customers);
        }
    }
}
