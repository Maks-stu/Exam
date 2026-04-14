using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Interfaces;

namespace pgDataAccess.CRUDs.Services
{
    public class SOrderItems : IOrderItems
    {
        ApplicationContext _context;
        public SOrderItems()
        {
            _context = new ApplicationContext();
        }
        public OrderItem Create(OrderItem item)
        {
            try
            {
                var existingEntity = _context.OrderItems
                    .Local
                    .FirstOrDefault(e => e.OrderId == item.OrderId && e.ProductArticle == item.ProductArticle);

                if (existingEntity != null)
                {
                    return existingEntity;
                }
                _context.OrderItems.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public OrderItem ReadId(int id)
        {
            try
            {
                OrderItem entity = _context.OrderItems.Find(id);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<OrderItem> ReadAll()
        {
            try
            {
                List<OrderItem> entitylist = _context.OrderItems.ToList();
                return entitylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public OrderItem Update(int id, string article, OrderItem newitem)
        {
            try
            {
                var existingItem = _context.OrderItems.Find(id, article);
                if (existingItem == null)
                    return null;

                if (existingItem.ProductArticle != newitem.ProductArticle)
                {
                    Delete(existingItem.OrderId, existingItem.ProductArticle);
                    var newOrderItem = new OrderItem
                    {
                        OrderId = newitem.OrderId,
                        ProductArticle = newitem.ProductArticle,
                        Quantity = newitem.Quantity
                    };
                    return Create(newOrderItem);
                }
                else
                {
                    existingItem.Quantity = newitem.Quantity;
                    _context.SaveChanges();
                    return existingItem;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Delete(int order_id, string product_article)
        {
            try
            {
                OrderItem entity = _context.OrderItems.Where(x => x.OrderId == order_id && x.ProductArticle == product_article).First();
                if (entity != null)
                {
                    _context.OrderItems.Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
