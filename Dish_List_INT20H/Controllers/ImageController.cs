namespace Dish_List_INT20H.Controllers
{
    public static class ImageController
    {
        public static IResult GetImage(string path)
        {
            path = "./Images/" + path;
            Byte[] b = System.IO.File.ReadAllBytes(path);
            return Results.File(b, "image/jpeg");
        }
    }
}
