using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace pgDataAccess.ClassLibrary.Classes
{
    [DataContract]
    [Table("orders")]
    public class Order
    {
        [DataMember]
        [Column("id")]
        [Key]
        public int Id { get; set; }


        [DataMember]
        [Column("client_id")]
        public int ClientId { get; set; }


        [DataMember]
        [Column("pickup_point_id")]
        public int PickupPointId { get; set; }


        [DataMember]
        [Column("order_date")]
        public DateOnly OrderDate { get; set; }


        [DataMember]
        [Column("delivery_date")]
        public DateOnly DeliveryDate { get; set; }


        [DataMember]
        [Column("status")]
        public string Status { get; set; }


        [DataMember]
        [Column("pickup_code")]
        public int PickupCode { get; set; }
    }
}
