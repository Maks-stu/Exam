using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Interfaces;

namespace pgDataAccess.CRUDs.Services
{
    public class SRoles : IRoles
    {
        ApplicationContext _context;
        public SRoles()
        {
            _context = new ApplicationContext();
        }
        public Role Create(Role item)
        {
            try
            {
                Role entity = item;
                entity.Id = _context.Roles.Max(x => x.Id) + 1;
                _context.Roles.Add(item);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Role ReadId(int id)
        {
            try
            {
                Role entity = _context.Roles.Find(id);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Role> ReadAll()
        {
            try
            {
                List<Role> entitylist = _context.Roles.ToList();
                return entitylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Role Update(int id, Role newitem)
        {
            try
            {
                Role entity = _context.Roles.Find(id);
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
                Role entity = _context.Roles.Find(id);
                if (entity != null)
                {
                    _context.Roles.Remove(entity);
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
