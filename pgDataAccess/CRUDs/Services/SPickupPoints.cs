using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Interfaces;

namespace pgDataAccess.CRUDs.Services
{
    public class SPickupPoints : IPickupPoints
    {
        ApplicationContext _context;
        public SPickupPoints()
        {
            _context = new ApplicationContext();
        }
        public PickupPoint Create(PickupPoint item)
        {
            try
            {
                PickupPoint entity = item;
                entity.Id = _context.PickupPoints.Max(x => x.Id) + 1;
                _context.PickupPoints.Add(item);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PickupPoint ReadId(int id)
        {
            try
            {
                PickupPoint entity = _context.PickupPoints.Find(id);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PickupPoint> ReadAll()
        {
            try
            {
                List<PickupPoint> entitylist = _context.PickupPoints.ToList();
                return entitylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PickupPoint Update(int id, PickupPoint newitem)
        {
            try
            {
                PickupPoint entity = _context.PickupPoints.Find(id);
                entity.Address = newitem.Address;
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
                PickupPoint entity = _context.PickupPoints.Find(id);
                if (entity != null)
                {
                    _context.PickupPoints.Remove(entity);
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
