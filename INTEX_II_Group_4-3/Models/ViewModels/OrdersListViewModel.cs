namespace INTEX_II_Group_4_3.Models.ViewModels
{
    public class OrdersListViewModel
    {
        public IQueryable<Order> Orders { get; set;}

        public PaginationInfo PaginationInfo { get; set;} = new PaginationInfo();
    }
}
