using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastucture.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product)
        {
            return product == null ? null : new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand?.Name,
                Category = product.Category?.Name
            };
        }

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products)
        {
            return products.Select(ToView);
        }
    }
}
