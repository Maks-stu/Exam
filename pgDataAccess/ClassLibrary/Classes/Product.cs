using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

[DataContract]
[Table("products")]
public class Product
{
    [DataMember]
    [Column("article")]
    [Key]
    public string Article { get; set; }

    [DataMember]
    [Column("name")]
    public string Name { get; set; }

    [DataMember]
    [Column("unit")]
    public string Unit { get; set; }

    [DataMember]
    [Column("price")]
    public double Price { get; set; }

    [DataMember]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [DataMember]
    [Column("manufacturer_id")]
    public int ManufacturerId { get; set; }

    [DataMember]
    [Column("supplier_id")]
    public int SupplierId { get; set; }

    [DataMember]
    [Column("discount_percent")]
    public int DiscountPercent { get; set; }

    [DataMember]
    [Column("stock_quantity")]
    public int StockQuantity { get; set; }

    [DataMember]
    [Column("description")]
    public string Description { get; set; }

    [DataMember]
    [Column("photo_path")]
    public string? PhotoPath { get; set; }
}