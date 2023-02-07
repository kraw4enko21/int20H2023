using System.Text.Json.Serialization;

namespace Dish_List_INT20H.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumTypes
    {
        Grams,
        Pieces,
        Millilitres
    }
}
