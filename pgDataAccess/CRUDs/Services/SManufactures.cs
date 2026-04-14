using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Interfaces;

namespace pgDataAccess.CRUDs.Services
{
    public class SManufactures : IManufactures
    {
        ApplicationContext _context;
        public SManufactures()
        {
            _context = new ApplicationContext();
        }
        public Manufacture Create(Manufacture item)
        {
            try
            {
                Manufacture entity = item;
                entity.Id = _context.Manufactures.Max(x => x.Id) + 1;
                _context.Manufactures.Add(item);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Manufacture ReadId(int id)
        {
            try
            {
                Manufacture entity = _context.Manufactures.Find(id);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Manufacture> ReadAll()
        {
            try
            {
                List<Manufacture> entitylist = _context.Manufactures.ToList();
                return entitylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Manufacture Update(int id, Manufacture newitem)
        {
            try
            {
                Manufacture entity = _context.Manufactures.Find(id);
                entity.Name = newitem.Name;
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
                Manufacture entity = _context.Manufactures.Find(id);
                if(entity != null)
                {
                    _context.Manufactures.Remove(entity);
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
