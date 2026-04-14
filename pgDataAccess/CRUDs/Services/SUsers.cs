using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Interfaces;

namespace pgDataAccess.CRUDs.Services
{
    public class SUsers : IUsers
    {
        ApplicationContext _context;
        public SUsers()
        {
            _context = new ApplicationContext();
        }
        public User Create(User item)
        {
            try
            {
                User entity = item;
                entity.Id = _context.Users.Max(x => x.Id) + 1;
                _context.Users.Add(item);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User ReadId(int id)
        {
            try
            {
                User entity = _context.Users.Find(id);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> ReadAll()
        {
            try
            {
                List<User> entitylist = _context.Users.ToList();
                return entitylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User Update(int id, User newitem)
        {
            try
            {
                User entity = _context.Users.Find(id);
                entity.Login = newitem.Login;
                entity.PasswordHash = newitem.PasswordHash;
                entity.FullName = newitem.FullName;
                entity.RoleId = newitem.RoleId;
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
                User entity = _context.Users.Find(id);
                if (entity != null)
                {
                    _context.Users.Remove(entity);
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
