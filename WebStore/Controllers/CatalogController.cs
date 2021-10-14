using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Infrastucture.Mapping;
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
                Products = products.OrderBy(p => p.Order).ToView()
            };

            return View(catalogVM);
        }

        public IActionResult Details(int id)
        {
            var product = _productData.GetProductById(id);

            if (product == null)
                return NotFound();

            return View(product.ToView());
        }

    }
}
