
namespace INTEX_II_Group_4_3.Models
{
    public class EFLegoRepository : ILegoRepository
    {
        private LegoInfoContext _context;
        public EFLegoRepository(LegoInfoContext temp) { 
            _context = temp;
        }
        public IQueryable<Product> Products => _context.Products;

        //add order through checkout
        public void AddOrder(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();
        }
    }
}
