using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Category> GetCategories()
        {
            return TestData.Categories;
        }

        public Category GetCategoryById(int id)
        {
            return TestData.Categories.FirstOrDefault(p => p.Id == id);
        }
        
        public Brand GetBrandById(int id)
        {
            return TestData.Brands.FirstOrDefault(p => p.Id == id);
        }

        public Product GetProductById(int id)
        {
            return TestData.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IEnumerable<Product> query = TestData.Products;

            if (filter?.CategoryId != null)
                query = query.Where(p => p.CategoryId == filter.CategoryId);

            if (filter?.BrandId != null)
                query = query.Where(p => p.BrandId == filter.BrandId);

            return query;
        }
    }
}
