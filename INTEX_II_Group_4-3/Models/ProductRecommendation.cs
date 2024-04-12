using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTEX_II_Group_4_3.Models
{
    public class ProductRecommendation
    {
        [Key]
        [ForeignKey("ProductRec")]
        public int Product_ID { get; set; }
        //ProductRec is used for referencing in the views
        public Product ProductRec { get; set; }

        [ForeignKey("Product_1")]
        public int Recommendation_1 { get; set; }
        public Product Product_1 { get; set; }

        [ForeignKey("Product_2")]
        public int Recommendation_2 { get; set; }
        public Product Product_2 { get; set; }

        [ForeignKey("Product_3")]
        public int Recommendation_3 { get; set; }
        public Product Product_3 { get; set; }

        [ForeignKey("Product_4")]
        public int Recommendation_4 { get; set; }
        public Product Product_4 { get; set; }

        [ForeignKey("Product_5")]
        public int Recommendation_5 { get; set; }
        public Product Product_5 { get; set; }

    }
}
