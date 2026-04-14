using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;

namespace pgDataAccess.CRUDs.Interfaces
{
    public interface ISuppliers
    {
        public Supplier Create(Supplier item);
        public Supplier ReadId(int id);
        public List<Supplier> ReadAll();
        public Supplier Update(int id, Supplier newitem);
        public bool Delete(int id);

    }
}
