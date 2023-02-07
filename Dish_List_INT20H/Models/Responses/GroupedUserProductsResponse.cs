namespace Dish_List_INT20H.Models.Responses
{
    public class GroupedUserProductsResponse
    {
    }

    public class GroupedProducts
    {
        public ProductCategories ProductCategories { get; set; }

        public List<ProductQuantity> Products { get; set;}
    }
}
