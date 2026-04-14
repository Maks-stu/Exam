using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;

namespace pgDataAccess.CRUDs.Interfaces
{
    public interface IOrders
    {
        public Order Create(Order item);
        public Order ReadId(int id);
        public List<Order> ReadAll();
        public Order Update(int id, Order newitem);
        public bool Delete(int id);

    }
}
