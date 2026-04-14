using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Interfaces;

namespace pgDataAccess.CRUDs.Services
{
    public class SOrders : IOrders
    {
        ApplicationContext _context;
        public SOrders()
        {
            _context = new ApplicationContext();
        }
        public Order Create(Order item)
        {
            try
            {
                Order entity = item;
                entity.Id = _context.Orders.Max(x => x.Id) + 1;
                _context.Orders.Add(item);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Order ReadId(int id)
        {
            try
            {
                Order entity = _context.Orders.Find(id);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Order> ReadAll()
        {
            try
            {
                List<Order> entitylist = _context.Orders.ToList();
                return entitylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Order Update(int id, Order newitem)
        {
            try
            {
                Order entity = _context.Orders.Find(id);
                entity.ClientId = newitem.ClientId;
                entity.PickupPointId = newitem.PickupPointId;
                entity.OrderDate = newitem.OrderDate;
                entity.DeliveryDate = newitem.DeliveryDate;
                entity.Status = newitem.Status;
                entity.PickupCode = newitem.PickupCode;
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Order entity = _context.Orders.Find(id);
                if (entity != null)
                {
                    _context.Orders.Remove(entity);
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
