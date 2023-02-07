namespace Dish_List_INT20H.Models.Payloads
{
    public class DeleteUserProductPayload
    {
        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }
    }
}
