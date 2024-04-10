using INTEX_II_Group_4_3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using INTEX_II_Group_4_3.Models.ViewModels;


namespace INTEX_II_Group_4_3.Controllers
{
    public class HomeController : Controller
    {
        private ILegoRepository _repo;

        public HomeController(ILegoRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Shop(int pageNum, string? productCategory)
        {
            int pageSize = 5;
            pageNum = Math.Max(pageNum, 1);

            var blah = new ProductsListViewModel
            {
                Products = _repo.Products
                .Where(x => x.Category == productCategory || productCategory == null)
                .OrderBy(x => x.Name)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = productCategory == null ? _repo.Products.Count() : _repo.Products.Where(x => x.Category == productCategory).Count()
                },

                CurrentProductCategory = productCategory
            };

            return View(blah);
        }

        //private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger, LegoInfoContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            //var products = await _context.Products.ToListAsync();
            ////return View(products);

            return View();
        }



        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Confirmation()
        {
            return View();
        }

        [Authorize]
        public IActionResult Fraudulent()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Render the Checkout View
        [HttpGet]
        public IActionResult Checkout()
        {
            //autofill name from Customer table
            //ViewBag.first_name = _context.Customers.ToList();
            //ViewBag.last_name = _context.Customers.ToList();
            return View("Checkout");
        }

        // Post Checkout to database
        [HttpPost]
        public IActionResult Order(Order response)
        {
           // _context.Orders.Add(response);
            //_context.SaveChanges();

            return View("Confirmation", response);

        }
    }
}
