namespace INTEX_II_Group_4_3.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IQueryable<Product> Products { get; set;}

        public PaginationInfo PaginationInfo { get; set;} = new PaginationInfo();

        public string? CurrentProductCategory { get; set;}
        public string? CurrentProductColor { get; set;}
    }
}
