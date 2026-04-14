using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;

namespace pgDataAccess.CRUDs.Interfaces
{
    public interface IUsers
    {
        public User Create(User item);
        public User ReadId(int id);
        public List<User> ReadAll();
        public User Update(int id, User newitem);
        public bool Delete(int id);

    }
}
