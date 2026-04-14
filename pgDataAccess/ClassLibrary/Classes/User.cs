using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace pgDataAccess.ClassLibrary.Classes
{
    [DataContract]
    [Table("users")]
    public class User
    {
        [DataMember]
        [Column("id")]
        [Key]
        public int Id { get; set; }


        [DataMember]
        [Column("login")]
        public string Login { get; set; }


        [DataMember]
        [Column("password_hash")]
        public string PasswordHash { get; set; }


        [DataMember]
        [Column("full_name")]
        public string FullName { get; set; }


        [DataMember]
        [Column("role_id")]
        public int RoleId { get; set; }
    }
}
