using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pgDataAccess.ClassLibrary.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Article { get; set; }
        public string Status { get; set; }
        public string PickupPoint { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
    }
}
