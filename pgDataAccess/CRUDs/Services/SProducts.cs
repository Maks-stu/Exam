using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Interfaces;

namespace pgDataAccess.CRUDs.Services
{
    public class SProducts : IProducts
    {
        ApplicationContext _context;
        public SProducts()
        {
            _context = new ApplicationContext();
        }
        public Product Create(Product item)
        {
            try
            {
                var existingProduct = _context.Products
                    .FirstOrDefault(p => p.Article == item.Article);

                if (existingProduct != null)
                {
                    return existingProduct;
                }
                else
                {
                    _context.Products.Add(item);
                    _context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Product ReadId(string article)
        {
            try
            {
                Product entity = _context.Products.Find(article);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Product> ReadAll()
        {
            try
            {
                List<Product> entitylist = _context.Products.ToList();
                return entitylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Product Update(string article, Product newitem)
        {
            try
            {
                Product entity = _context.Products.Find(article);
                entity.Name = newitem.Name;
                entity.Unit = newitem.Unit;
                entity.Price = newitem.Price;
                entity.CategoryId = newitem.CategoryId;
                entity.ManufacturerId = newitem.ManufacturerId;
                entity.SupplierId = newitem.SupplierId;
                entity.DiscountPercent = newitem.DiscountPercent;
                entity.StockQuantity = newitem.StockQuantity;
                entity.Description = newitem.Description;
                entity.PhotoPath = newitem.PhotoPath;
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Delete(string article)
        {
            try
            {
                Product entity = _context.Products.Find(article);
                if (entity != null)
                {
                    _context.Products.Remove(entity);
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
