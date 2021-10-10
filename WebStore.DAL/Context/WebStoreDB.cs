using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;

namespace WebStore.DAL
{
    /// <summary>
    /// TODO: в идеале сделать два контекста (каталог товаров и пользователи и роли)
    /// </summary>
    public class WebStoreDB: IdentityDbContext<User, Role, string>// собственные классы пользователя и роли, тип первичного ключа
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
