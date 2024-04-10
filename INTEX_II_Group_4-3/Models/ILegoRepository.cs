namespace INTEX_II_Group_4_3.Models
{
    public interface ILegoRepository
    {
        public IQueryable<Product> Products { get; }

        void AddOrder(Order o);
    }
}
