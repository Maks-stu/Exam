using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;

namespace pgDataAccess.CRUDs.Interfaces
{
    public interface IOrderItems
    {
        public OrderItem Create(OrderItem item);
        public OrderItem ReadId(int id);
        public List<OrderItem> ReadAll();
        public OrderItem Update(int id, string article, OrderItem newitem);
        public bool Delete(int order_id, string product_article);

    }
}
