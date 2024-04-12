using INTEX_II_Group_4_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace INTEX_II_Group_4_3.Components
{
    public class ProductColorViewComponent : ViewComponent
    {
        private ILegoRepository _legoRepository;
        public ProductColorViewComponent(ILegoRepository temp)
        {
            _legoRepository = temp;
        }
        public IViewComponentResult Invoke()
        {
            var producColor = _legoRepository.Products
                .Select(x => x.PrimaryColor)
                .Distinct()
                .OrderBy(x => x);

            return View(producColor);
        }
    }
}
