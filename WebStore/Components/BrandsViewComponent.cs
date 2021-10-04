using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    //[ViewComponent(Name = "BrandsView")]
    public class BrandsViewComponent: ViewComponent
    {
        private readonly IProductData _productData;
        public BrandsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        //public async Task<IViewComponentResult> Invoke() => View();
        public IViewComponentResult Invoke()
        {
            return View(GetBrands());
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            return _productData.GetBrands().OrderBy(p => p.Order)
                .Select(s => new BrandViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                });
        }
    }
}
