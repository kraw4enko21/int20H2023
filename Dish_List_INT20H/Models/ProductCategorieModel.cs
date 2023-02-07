using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dish_List_INT20H.Models
{
    public class ProductCategories
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public override bool Equals(object obj)
        {
            var item = obj as ProductCategories;

            if (item == null)
            {
                return false;
            }

            return this.Title.Equals(item.Title);
        }
        public override int GetHashCode()
        {
            return this.Title.GetHashCode();
        }
    }
}
