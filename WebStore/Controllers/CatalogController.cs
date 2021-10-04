using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;
        public CatalogController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index(int? BrandId, int? CategoryId)
        {
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                CategoryId = CategoryId
            };

            var products = _productData.GetProducts(filter);

            var catalogVM = new CatalogViewModel
            {
                BrandId = BrandId,
                CategoryId = CategoryId,
                Products = products.OrderBy(p => p.Order).Select(s => new ProductViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl
                })
            };

            return View(catalogVM);
        }
    }
}
