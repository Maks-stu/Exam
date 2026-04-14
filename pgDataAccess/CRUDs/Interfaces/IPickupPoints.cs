using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pgDataAccess.ClassLibrary.Classes;

namespace pgDataAccess.CRUDs.Interfaces
{
    public interface IPickupPoints
    {
        public PickupPoint Create(PickupPoint item);
        public PickupPoint ReadId(int id);
        public List<PickupPoint> ReadAll();
        public PickupPoint Update(int id, PickupPoint newitem);
        public bool Delete(int id);

    }
}
