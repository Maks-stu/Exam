using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Interfaces;

namespace pgDataAccess.CRUDs.Services
{
    public class SCategories : ICategories
    {
        ApplicationContext _context;
        public SCategories()
        {
            _context = new ApplicationContext();
        }
        public Category? Create(Category item)
        {
            try
            {
                Category entity = item;
                entity.Id = _context.Categories.Max(x => x.Id) + 1;
                _context.Categories.Add(item);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Category? ReadId(int id)
        {
            try
            {
                Category entity = _context.Categories.Find(id);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Category> ReadAll()
        {
            try
            {
                List<Category> entitylist = _context.Categories.ToList();
                return entitylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Category Update(int id, Category newitem)
        {
            try
            {
                Category entity = _context.Categories.Find(id);
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
                Category entity = _context.Categories.Find(id);
                if(entity != null)
                {
                    _context.Categories.Remove(entity);
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
