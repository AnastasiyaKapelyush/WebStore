using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly IProductData _productData;

        public CategoryViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _productData.GetCategories();

            var parentCategories = categories.Where(c => c.ParentId == null);

            var parentCategoriesView = parentCategories.Select(
                s => new CategoryViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order
                }).ToList();

            foreach (var parentCategory in parentCategoriesView)
            {
                var childCategories = categories.Where(c => c.ParentId == parentCategory.Id);

                foreach (var childCategory in childCategories)
                    parentCategory.ChildCategories.Add(
                        new CategoryViewModel
                        {
                            Id = childCategory.Id,
                            Name = childCategory.Name,
                            Order = childCategory.Order,
                            Parent = parentCategory
                        });

                parentCategory.ChildCategories.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parentCategoriesView.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return View(parentCategoriesView);
        }
    }
}
