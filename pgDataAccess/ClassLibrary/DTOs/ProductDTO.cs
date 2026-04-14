using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace pgDataAccess.ClassLibrary.DTOs
{
    public class ProductDTO
    {
        public string Article { get; set; }
        public string BackGround { get; set; }
        public string Image { get; set; }
        public string Category {  get; set; }
        public string Name {  get; set; }
        public string Description {  get; set; }
        public string Manufacturer {  get; set; }
        public string Supplier {  get; set; }
        public string OldPriceBG { get; set; }
        public string OldPriceLine { get; set; }
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
        public string Dimension {  get; set; }
        public string Count {  get; set; }
        public string Discount {  get; set; }
    }
}
