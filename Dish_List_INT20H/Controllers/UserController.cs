using Dish_List_INT20H.Database;
using Dish_List_INT20H.Models;
using Dish_List_INT20H.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dish_List_INT20H.Controllers
{
    public static class UserController
    {
        public async static Task<List<User>> GetUsersAsync()
        {
            using(var db = new AppDbContext())
            {
                return await db.Users.Include(us => us.Products).ThenInclude(prQ => prQ.Product).ThenInclude(pr => pr.Category).ToListAsync();
            }
        }
        public async static Task<List<ProductQuantity>> GetUsersProduct(Guid userId)
        {
            using (var db = new AppDbContext())
            {
                var user = await db.Users.Where(us => us.Id == userId).Include(x => x.Products).ThenInclude(prQ => prQ.Product).ThenInclude(pr => pr.Category).FirstOrDefaultAsync();
                return user.Products;
            }
        }

        public async static Task<GetItemsList<GroupedProducts>> GetGroupedUsersProduct(Guid userId)
        {
            User user;
            using (var db = new AppDbContext())
            {
                 user = await db.Users.Where(us => us.Id == userId).Include(x => x.Products).ThenInclude(prQ => prQ.Product).ThenInclude(pr => pr.Category).FirstOrDefaultAsync();
            }

            var userProducts = user.Products.ToList();

            var groupProducts = userProducts.GroupBy(x => x.Product.Category).ToList();

            List<GroupedProducts> groupUserProducts = new List<GroupedProducts>();

            foreach (var group in groupProducts)
            {
                groupUserProducts.Add(new GroupedProducts()
                {
                    ProductCategories = group.Key,
                    Products = group.ToList()
                });
            }
            
            return new GetItemsList<GroupedProducts>(groupUserProducts);

        }

        public async static Task<List<ProductQuantity>> AddUserProduct(Guid userId, Guid productId, int quantity)
        {
            using(var db = new AppDbContext())
            {
                var user = await db.Users.Where(x => x.Id == userId).Include(x => x.Products).ThenInclude(prQ => prQ.Product).ThenInclude(pr => pr.Category).FirstOrDefaultAsync();
                var product = user.Products.FirstOrDefault(x => x.Product.Id == productId);
                if (product != null)
                {
                    product.Quantity = quantity;
                }
                else
                {
                    product = new ProductQuantity()
                    {
                        Product = await db.Products.FirstOrDefaultAsync(x => x.Id == productId),
                        Quantity = quantity
                    };
                    user.Products.Add(product);
                }
                db.SaveChanges();
            }
            return await GetUsersProduct(userId);
        }

        public async static Task<List<ProductQuantity>> ChangeUserProductQuantity(Guid userId, Guid productId, int quantity)
        {
            using (var db = new AppDbContext())
            {
                var user = await db.Users.Where(x => x.Id == userId).Include(x => x.Products).ThenInclude(prQ => prQ.Product).ThenInclude(pr => pr.Category).FirstOrDefaultAsync();
                var product = user.Products.FirstOrDefault(x => x.Product.Id == productId);
                product.Quantity = quantity;
                db.SaveChanges();
            }
            return await GetUsersProduct(userId);
        }

        public async static Task<List<ProductQuantity>> DeleteUserProduct(Guid userId, Guid productId)
        {
            using (var db = new AppDbContext())
            {
                var user = await db.Users.Where(x => x.Id == userId).Include(x => x.Products).ThenInclude(prQ => prQ.Product).ThenInclude(pr => pr.Category).FirstOrDefaultAsync();
                var product = user.Products.FirstOrDefault(x => x.Product.Id == productId);
                user.Products.Remove(product);
                db.SaveChanges();
            }
            return await GetUsersProduct(userId);
        }

        public async static void GenerateUserInstanse()
        {
            using (var db = new AppDbContext())
            {
                User user = new User()
                {
                    Id = new Guid("9abbcd42-7698-4edb-bd60-9ec72e59c8c8"),
                    Title = "Artur"
                };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}
