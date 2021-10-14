using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db)
        {
            _db = db;
        }

        public Brand GetBrandById(int id)
        {
            return _db.Brands.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _db.Brands;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }

        public Category GetCategoryById(int id)
        {
            return _db.Categories.FirstOrDefault(c => c.Id == id);
        }

        public Product GetProductById(int id)
        {
            return _db.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> query = _db.Products.Include(p => p.Brand).Include(p => p.Category);

            if (filter?.CategoryId != null)
                query = query.Where(p => p.CategoryId == filter.CategoryId);

            if (filter?.BrandId != null)
                query = query.Where(p => p.BrandId == filter.BrandId);

            return query;
        }
    }
}
