using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;

namespace pgDataAccess.CRUDs.Interfaces
{
    public interface IRoles
    {
        public Role Create(Role item);
        public Role ReadId(int id);
        public List<Role> ReadAll();
        public Role Update(int id, Role newitem);
        public bool Delete(int id);

    }
}
