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
    [Table("manufacturers")]
    public class Manufacture
    {
        [DataMember]
        [Column("id")]
        [Key]
        public int Id { get; set; }


        [DataMember]
        [Column("name")]
        public string Name { get; set; }
    }
}
