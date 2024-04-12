using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace INTEX_II_Group_4_3.Models
{
    public class TopProductRecommendation
    {

        [Key]
        [ForeignKey("ProductRec")]
        public int product_ID { get; set; }
        //ProductRec is used for referencing in the views
        public Product ProductRec { get; set; }

        //[ForeignKey("Product_1")]
        public int ratings_count { get; set; }
        //public Product Product_1 { get; set; }

        //[ForeignKey("Product_2")]
        public double ratings_mean { get; set; }
        //public Product Product_2 { get; set; }

        //[ForeignKey("Product_3")]
        public string name { get; set; }
        //public Product Product_3 { get; set; }

        //[ForeignKey("Product_4")]
        //public double Recommendation_4 { get; set; }
        //public Product Product_4 { get; set; }

        //[ForeignKey("Product_5")]
        //public double Recommendation_5 { get; set; }
        //public Product Product_5 { get; set; }
    }
}
