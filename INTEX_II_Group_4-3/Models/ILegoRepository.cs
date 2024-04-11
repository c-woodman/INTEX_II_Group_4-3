namespace INTEX_II_Group_4_3.Models
{
    public interface ILegoRepository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Order> Orders { get; }
        public IEnumerable<FraudPrediction> FraudPredictions { get; }
        void AddOrder(Order o);
    }
}
