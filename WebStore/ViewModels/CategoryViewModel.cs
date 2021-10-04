using System.Collections.Generic;

namespace WebStore.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public CategoryViewModel Parent { get; set; }
        public List<CategoryViewModel> ChildCategories { get; set; } = new();
    }
}
