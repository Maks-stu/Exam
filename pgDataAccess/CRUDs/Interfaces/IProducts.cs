using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;

namespace pgDataAccess.CRUDs.Interfaces
{
    public interface IProducts
    {
        public Product Create(Product item);
        public Product ReadId(string article);
        public List<Product> ReadAll();
        public Product Update(string article, Product newitem);
        public bool Delete(string article);
    }
}
