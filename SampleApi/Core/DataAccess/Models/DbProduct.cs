using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApi.Core.DataAccess.Models
{
    [Table(TableName)]
    public class DbProduct
    {
        public const string TableName = "Products";

        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Name")]
        public string Name { get; set; } = null!;

        [Column("Quantity")]
        public int Quantity { get; set; }
    }
}
