using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace pgDataAccess.ClassLibrary.Classes
{
    [DataContract]
    [Table("order_items")]
    public class OrderItem
    {
        [DataMember]
        [Column("order_id")]
        public int OrderId { get; set; }


        [DataMember]
        [Column("product_article")]
        public string ProductArticle { get; set; }


        [DataMember]
        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
