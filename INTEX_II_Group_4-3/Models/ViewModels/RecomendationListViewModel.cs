namespace INTEX_II_Group_4_3.Models.ViewModels
{

    public class RecommendationListViewModel
    {
        // Use CleanProductViewModel instead of CleanProduct to include category information
        public IEnumerable<TopProductRecommendation> TopProductRecommendations { get; set; } = new List<TopProductRecommendation>();
        public IEnumerable<UserProductRecommendation> UserProductRecommendations { get; set; } = new List<UserProductRecommendation>();
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}