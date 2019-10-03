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
    public class ItemManager
    {
        ItemRepository _itemRepository = new ItemRepository();

        public string CanBeAdded(Items items)
        {

            return _itemRepository.CanBeAdded(items);
        }

        public string AddToRepository(Items items)
        {
            return _itemRepository.AddToRepository(items);
        }

        public DataTable ShowAll()
        {
            return _itemRepository.ShowAll();
        }

        public string Update(Items items)
        {
            return _itemRepository.Update(items);
        }

        public string Delete(Items items)
        {
            return _itemRepository.Delete(items);
        }

        public DataTable Search(Items items)
        {
            return _itemRepository.Search(items);
        }
    }
}
