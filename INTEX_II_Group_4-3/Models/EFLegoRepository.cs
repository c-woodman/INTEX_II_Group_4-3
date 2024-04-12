
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
        public IQueryable<Order> Orders => _context.Orders;

        public IQueryable<UserProductRecommendation> UserProductRecommendations => _context.UserBasedRecommendations;

        public IQueryable<TopProductRecommendation> TopProductRecommendations => _context.TopProductRecommendations;


        //admin edit product table
        public void AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
        public void RemoveProduct(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Product GetProductById(int productId)
        {
            var todo = _context.Products
                .FirstOrDefault(t => t.ProductId == productId);

            if (todo == null)
            {
                throw new InvalidOperationException($"No productId found with ID {productId}.");
            }

            return todo;
        }

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

        //public IQueryable<TopProductRecommendation> TopProductRecommendations(int productID) => _context.TopProductRecommendations
        //                                                                .Where(x => x.product_ID == productID)
        //                                                                //.Include(x => x.Product_1)
        //                                                                //.Include(x => x.Product_2)
        //                                                                //.Include(x => x.Product_3)
        //                                                                //.Include(x => x.Product_4)
        //                                                                //.Include(x => x.Product_5)
        //                                                                .Include(x => x.ProductRec);
        // In EFLegoRepository


        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

       }
}
