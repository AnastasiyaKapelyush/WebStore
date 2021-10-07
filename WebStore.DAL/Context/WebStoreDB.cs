using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;

namespace WebStore.DAL
{
    public class WebStoreDB: DbContext
    {
        //Описание свойств
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> options): base(options)
        {
        }

        //Метод описания модели
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
