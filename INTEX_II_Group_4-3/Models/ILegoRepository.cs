
namespace INTEX_II_Group_4_3.Models
{
    public interface ILegoRepository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Order> Orders { get; }


        public IQueryable<ProductRecommendation> ProductRecommendations(int productId);
        public IQueryable<TopProductRecommendation> TopProductRecommendations(int productId);
        public void AddProduct(Product product);
        public void RemoveProduct(Product product);
        public void UpdateProduct(Product product);
        public Product GetProductById(int productId);
        void AddOrder(Order o);
        Task SaveChangesAsync();
    }
}
