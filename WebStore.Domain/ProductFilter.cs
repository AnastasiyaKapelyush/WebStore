namespace WebStore.Domain
{
    public class ProductFilter
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int[] Ids { get; set; }
    }
}
