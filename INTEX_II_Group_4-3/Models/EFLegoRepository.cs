
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

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

        // For pulling recommendations for each product
        public IQueryable<ProductRecommendation> ProductRecommendations(int productID) => _context.ProductRecommendations
                                                                                .Where(x => x.Product_ID == productID)
                                                                                .Include(x => x.Product_1)
                                                                                .Include(x => x.Product_2)
                                                                                .Include(x => x.Product_3)
                                                                                .Include(x => x.Product_4)
                                                                                .Include(x => x.Product_5)
                                                                                .Include(x => x.ProductRec);

    }
}
