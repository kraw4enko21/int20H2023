using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dish_List_INT20H.Models
{
    public class Dish
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public int CookingTime { get; set; }

        public string Descriprion { get; set; }

        public DishNationalities Nationality { get; set; }

        public int Complexity { get; set; }

        public int Rate { get; set; }

        public string Recipe { get; set; }

        public List<ProductQuantity> Products { get; set; }
    }
}
