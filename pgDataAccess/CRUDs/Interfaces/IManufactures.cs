using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;

namespace pgDataAccess.CRUDs.Interfaces
{
    public interface IManufactures
    {
        public Manufacture Create(Manufacture item);
        public Manufacture ReadId(int id);
        public List<Manufacture> ReadAll();
        public Manufacture Update(int id, Manufacture newitem);
        public bool Delete(int id);

    }
}
