using Dish_List_INT20H.Database;
using Dish_List_INT20H.Models;
using Dish_List_INT20H.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dish_List_INT20H.Controllers
{
    public static class ProductController
    {
        public async static Task<GetItemsList<Product>> GetProductsAsync()
        {
            using (var db = new AppDbContext())
            {
                var products = await db.Products.Include(pr => pr.Category).ToListAsync();
                GetItemsList<Product> responseBody = new GetItemsList<Product>(products);
                return responseBody;
            }
        }
        public async static Task<Product> GetProductByIdAsync(Guid id)
        {
            using (var db = new AppDbContext())
            {
                return await db.Products.Where(x => x.Id == id).Include(pr => pr.Category).FirstOrDefaultAsync();
            }
        }
        public async static Task<bool> CreateProductAsync(Product product)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    await db.Products.AddAsync(product);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        #region TECH
        public async static void GenerateInstatceInDB()
        {
            using (var db = new AppDbContext())
            {
                Product[] products = new Product[39];
                products[0] = new Product()
                {
                    Title = "Яйця",
                    MeasurmentType = EnumTypes.Pieces,
                    Image = "egg.jpg",
                    Category = FindOrCreate(db.ProductCategories, "Бакалія")
                };
                products[1] = new Product()
                {
                    Title = "Рослинна олія",
                    MeasurmentType = EnumTypes.Millilitres,
                    Image = "oil.jpg",
                    Category = FindOrCreate(array: db.ProductCategories, "Бакалія")
                };
                products[2] = new Product()
                {
                    Title = "Вершкове масло",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "butter.jpg",
                    Category = FindOrCreate(array: db.ProductCategories, "Молочні вироби")
                };
                products[3] = new Product()
                {
                    Title = "Цукор",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "shugar.jpg",
                    Category = FindOrCreate(array: db.ProductCategories, "Бакалія")
                };
                products[4] = new Product()
                {
                    Title = "Сіль",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "salt.jpg",
                    Category = FindOrCreate(array: db.ProductCategories, "Бакалія")
                };
                products[5] = new Product()
                {
                    Title = "Рис",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "rice.jpg",
                    Category = FindOrCreate(array: db.ProductCategories, "Бакалія")
                };
                products[6] = new Product()
                {
                    Title = "Борошно",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "wheatFlour.jpg",
                    Category = FindOrCreate(array: db.ProductCategories, "Бакалія")
                };
                products[7] = new Product()
                {
                    Title = "Куряча грудинка",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "chickenBreast.jpg",
                    Category = FindOrCreate(array: db.ProductCategories, "М'ясо")
                };
                products[8] = new Product()
                {
                    Title = "Картопля",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "Potatoes.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Овочі")
                };
                products[9] = new Product()
                {
                    Title = "Лайм",
                    MeasurmentType = EnumTypes.Pieces,
                    Image = "lime.jpg",
                    Category = FindOrCreate(array: db.ProductCategories, "Фрукти")
                };
                products[10] = new Product()
                {
                    Title = "Цибуля",
                    MeasurmentType = EnumTypes.Pieces,
                    Image = "onions.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Овочі")
                };
                products[11] = new Product()
                {
                    Title = "Перець чилі",
                    MeasurmentType = EnumTypes.Pieces,
                    Image = "RedChilliPowder.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Овочі")
                };
                products[12] = new Product()
                {
                    Title = "Свинна лопатка",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "BeefFillet.png",
                    Category = FindOrCreate(array: db.ProductCategories, "М'ясо")
                };
                products[13] = new Product()
                {
                    Title = "Чорний перець",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "BlackPepper.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Приправи")
                };
                products[14] = new Product()
                {
                    Title = "Петрушка",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "Parsley.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Зелень")
                };
                products[15] = new Product()
                {
                    Title = "Вершки",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "HeavyСream.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Молочні продукти")
                };
                products[16] = new Product()
                {
                    Title = "Зубчиків часнику",
                    MeasurmentType = EnumTypes.Pieces,
                    Image = "garlic.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Овочі")
                };
                products[17] = new Product()
                {
                    Title = "Оливкова олія",
                    MeasurmentType = EnumTypes.Millilitres,
                    Image = "oliveOil.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Бакалія")
                };
                products[18] = new Product()
                {
                    Title = "Гриби печериці",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "mushrooms.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Овочі")
                };
                products[18] = new Product()
                {
                    Title = "Помідори",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "tomatoes.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Овочі")
                };
                products[19] = new Product()
                {
                    Title = "Фарш яловичини",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "leanMincedBeef.png",
                    Category = FindOrCreate(array: db.ProductCategories, "М'ясо")
                };
                products[20] = new Product()
                {
                    Title = "Вустерширський соус",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "worcestershireSauce.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Соуси")
                };
                products[21] = new Product()
                {
                    Title = "Спагетті",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "spaghetti.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Бакалія")
                };
                products[22] = new Product()
                {
                    Title = "Сир пармезан",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "parmesan.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Молочні вироби")
                };
                products[23] = new Product()
                {
                    Title = "Томатна паста",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "tomatoPuree.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Соуси")
                };
                products[24] = new Product()
                {
                    Title = "Бульйон з яловичини",
                    MeasurmentType = EnumTypes.Millilitres,
                    Image = "hotBeefStock.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Соуси")
                };
                products[25] = new Product()
                {
                    Title = "Орегано",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "driedOregano.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Приправи")
                };
                products[26] = new Product()
                {
                    Title = "Лосось",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "Salmon.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Риба")
                };
                products[27] = new Product()
                {
                    Title = "Соєвий соус",
                    MeasurmentType = EnumTypes.Millilitres,
                    Image = "SoySauce.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Соуси")
                };
                products[28] = new Product()
                {
                    Title = "Саке",
                    MeasurmentType = EnumTypes.Millilitres,
                    Image = "Sake.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Алкоголь")
                };
                products[29] = new Product()
                {
                    Title = "Кунжут",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "SesameSeed.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Бакалія")
                };
                products[30] = new Product()
                {
                    Title = "Мед",
                    MeasurmentType = EnumTypes.Millilitres,
                    Image = "Honey.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Соуси")
                };
                products[31] = new Product()
                {
                    Title = "М'ята",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "Mint.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Зелень")
                };
                products[32] = new Product()
                {
                    Title = "Шпинат",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "Spinach.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Зелень")
                };
                products[33] = new Product()
                {
                    Title = "Огірок",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "Cucumber.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Овочі")
                };
                products[34] = new Product()
                {
                    Title = "Авокадо",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "Avocado.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Фрукти")
                };
                products[35] = new Product()
                {
                    Title = "Молоко",
                    MeasurmentType = EnumTypes.Millilitres,
                    Image = "Milk.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Молочні вироби")
                };
                products[36] = new Product()
                {
                    Title = "Болгарський перець",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "RedPepper.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Овочі")
                };
                products[37] = new Product()
                {
                    Title = "Бекон",
                    MeasurmentType = EnumTypes.Grams,
                    Image = "Bacon.png",
                    Category = FindOrCreate(array: db.ProductCategories, "М'ясо")
                };
                products[38] = new Product()
                {
                    Title = "Яєчні жовтки",
                    MeasurmentType = EnumTypes.Pieces,
                    Image = "EggYolks.png",
                    Category = FindOrCreate(array: db.ProductCategories, "Бакалія")
                };
                for (int i = 0; i < products.Length; i++)
                {
                    await db.Products.AddAsync(products[i]);
                }
                db.SaveChangesAsync();



            }

        }
        public static DishNationalities FindOrCreateNationalities(DbSet<DishNationalities> array, string value)
        {
            var item = array.FirstOrDefault(x => x.Title == value);
            if (item == null)
            {
                return new DishNationalities() { Title = value };
            }
            else
            {
                return item;
            }
        }

        public static ProductQuantity[] AddProductsByListTitles(DbSet<Product> array, string[] productTitles)
        {
            var db = new AppDbContext();
            ProductQuantity[] productQuantities = new ProductQuantity[productTitles.Length];

            for (int i = 0; i < productTitles.Length; i++)
            {
                productQuantities[i] = new ProductQuantity()
                {
                    Product = array.FirstOrDefault(x => x.Title == productTitles[i]),
                    Quantity = i
                };
            }

            return productQuantities;
        }

        public static ProductCategories FindOrCreate(DbSet<ProductCategories> array, string value)
        {
            var item = array.FirstOrDefault(x => x.Title == value);
            if (item == null)
            {
                return new ProductCategories() { Title = value };
            }
            else
            {
                return item;
            }
        }
        #endregion
    }
}
