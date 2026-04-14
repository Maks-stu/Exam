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
    [Table("pickup_points")]
    public class PickupPoint
    {
        [DataMember]
        [Column("id")]
        [Key]
        public int Id { get; set; }


        [DataMember]
        [Column("address")]
        public string Address { get; set; }
    }
}
