using Dish_List_INT20H.Models;
using Microsoft.EntityFrameworkCore;

namespace Dish_List_INT20H.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ProductCategories> ProductCategories { get; set; }

        public DbSet<DishNationalities> DishNationalities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./Database/AppDb.db");
        }
    }
}
