namespace INTEX_II_Group_4_3.Models
{
    public interface ILegoRepository
    {
        public IQueryable<Product> Products { get; }

        public IQueryable<ProductRecommendation> ProductRecommendations(int productId);
        public IQueryable<TopProductRecommendation> TopProductRecommendations(int productId);

        void AddOrder(Order o);
    }
}
