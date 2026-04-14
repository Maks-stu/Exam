using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace pgDataAccess.ClassLibrary.DTOs
{
    public class OrderItemDTO
    {
        public int OrderId { get; set; }
        public string ProductArticle { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
    }
}
