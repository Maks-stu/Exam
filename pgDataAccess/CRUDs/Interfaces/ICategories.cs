using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;

namespace pgDataAccess.CRUDs.Interfaces
{
    public interface ICategories
    {
        public Category Create(Category item);
        public Category ReadId(int id);
        public List<Category> ReadAll();
        public Category Update(int id, Category newitem);
        public bool Delete(int id);

    }
}
