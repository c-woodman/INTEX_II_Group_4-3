using INTEX_II_Group_4_3.Infrastructure;
using INTEX_II_Group_4_3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;

namespace INTEX_II_Group_4_3.Pages
{
    public class CartModel : PageModel
    {
        private ILegoRepository _repo;
        public Cart Cart { get; set; }
        public CartModel(ILegoRepository temp, Cart cartService) 
        {
            _repo = temp;
            Cart = cartService;
        }
        public void OnGet()
        {
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(int Product_ID)
        {
            Product prod = _repo.Products
                .FirstOrDefault(x =>  x.ProductId == Product_ID);

            if (prod != null)
            {
                //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(prod, 1);
                //HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostRemove (int productId) 
        {
            Cart.RemoveLine(Cart.Lines.First(x => x.Product.ProductId == productId).Product);

            return RedirectToPage(Cart);
        }
    }
}
