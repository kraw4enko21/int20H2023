using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dish_List_INT20H.Models
{
    public class ProductQuantity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public Guid Id { get;set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
