using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dish_List_INT20H.Models
{
    public class Product
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public ProductCategories Category { get; set; }
        public EnumTypes MeasurmentType { get; set; }
    }
}
