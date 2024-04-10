using INTEX_II_Group_4_3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace INTEX_II_Group_4_3.Components
{
    public class ProductCategoryViewComponent: ViewComponent
    {
        private ILegoRepository _legoRepository;
        public ProductCategoryViewComponent(ILegoRepository temp) 
        {
            _legoRepository = temp;
        }
        public IViewComponentResult Invoke()
        {
            var productCategory = _legoRepository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(productCategory);
        }
    }
}
