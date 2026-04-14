using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Interfaces;

namespace pgDataAccess.CRUDs.Services
{
    public class SSuppliers : ISuppliers
    {
        ApplicationContext _context;
        public SSuppliers()
        {
            _context = new ApplicationContext();
        }
        public Supplier Create(Supplier item)
        {
            try
            {
                Supplier entity = item;
                entity.Id = _context.Suppliers.Max(x => x.Id) + 1;
                _context.Suppliers.Add(item);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Supplier ReadId(int id)
        {
            try
            {
                Supplier entity = _context.Suppliers.Find(id);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Supplier> ReadAll()
        {
            try
            {
                List<Supplier> entitylist = _context.Suppliers.ToList();
                return entitylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Supplier Update(int id, Supplier newitem)
        {
            try
            {
                Supplier entity = _context.Suppliers.Find(id);
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
                Supplier entity = _context.Suppliers.Find(id);
                if (entity != null)
                {
                    _context.Suppliers.Remove(entity);
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
