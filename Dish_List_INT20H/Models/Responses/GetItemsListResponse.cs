namespace Dish_List_INT20H.Models.Responses
{
    public class GetItemsList<T>
    {
        public GetItemsList(List<T> itemsList)
        {
            Total = itemsList.Count;
            Items = itemsList;
        }
        public int Total { get; set; }
        public List<T> Items { get; set; }
    }
}
