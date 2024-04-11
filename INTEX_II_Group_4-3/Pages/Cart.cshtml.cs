using INTEX_II_Group_4_3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INTEX_II_Group_4_3.Pages
{
    public class CartModel : PageModel
    {
        private ILegoRepository _repo;
        public CartModel(ILegoRepository temp) 
        {
            _repo = temp;
        }
        public Cart? Cart { get; set; }
        public void OnGet()
        {
        }
        public void OnPost(int productId)
        {
            Product prod = _repo.Products
                .FirstOrDefault(x =>  x.ProductId == productId);

            Cart = new Cart();

            Cart.AddItem(prod, 1);
        }
    }
}
