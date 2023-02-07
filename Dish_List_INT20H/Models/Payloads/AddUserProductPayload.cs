namespace Dish_List_INT20H.Models.Payloads
{
    public class AddUserProductPayload
    {
        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
