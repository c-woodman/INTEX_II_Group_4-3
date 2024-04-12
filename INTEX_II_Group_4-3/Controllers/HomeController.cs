using INTEX_II_Group_4_3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using INTEX_II_Group_4_3.Models.ViewModels;
using Microsoft.ML.OnnxRuntime;
using Elfie.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace INTEX_II_Group_4_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILegoRepository _repo;
        private readonly InferenceSession _session;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILegoRepository temp, ILogger<HomeController> logger)
        {
            _repo = temp;
            _logger = logger;

            try
            {
                _session = new InferenceSession(@"C:\Path\To\Your\FraudDetection.onnx");
                _logger.LogInformation("ONNX loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to load ONNX model: {ex.Message}");
            }
        }

        
        public IActionResult Shop(int pageNum, string? productCategory, string? productColor, int pageSize = 5)
        {
            pageNum = Math.Max(pageNum, 1);

            var blah = new ProductsListViewModel
            {
                Products = _repo.Products
                .Where(x => x.Category == productCategory || productCategory == null)
                .Where(x => x.PrimaryColor == productColor || productColor == null)
                .OrderBy(x => x.Name)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Products.Where(x => (x.Category == productCategory || productCategory == null) 
                        && (x.PrimaryColor == productColor || productColor == null)).Count()
                    //TotalItems = productCategory == null ? _repo.Products.Count() : _repo.Products.Where(x => x.Category == productCategory).Count()
                },

                CurrentProductCategory = productCategory,
                CurrentProductColor = productColor
            };

            return View(blah);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var productData = await _repo.ProductRecommendations(id)
                .FirstOrDefaultAsync(p => p.Product_ID == id);

            return View(productData);
        }

        //private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger, LegoInfoContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //}

        //public IActionResult Index(int productID)
        //{
        //    var recommendations = _repo.TopProductRecommendations(productID).FirstOrDefault(p => p.product_ID ==productID);
        //    return View(recommendations);
        //}

        //public async Task<IActionResult> Index(int productID)
        //{
        //    var recommendations = await _repo.TopProductRecommendations(productID)
        //        .FirstOrDefaultAsync(p => p.product_ID == productID);
        //        //.ToList() // Convert to a list

        //    return View(recommendations);
        //}

        //public async Task<IActionResult> Index(int productID)
        //{
        //    var recommendation = await _repo.TopProductRecommendations(productID)
        //        .FirstOrDefaultAsync(p => p.product_ID == productID);

        //    // Create a list with the single recommendation
        //    var recommendationsList = new List<TopProductRecommendation>();
        //    if (recommendation != null)
        //    {
        //        recommendationsList.Add(recommendation);
        //    }

        //    return View(recommendationsList);
        //}

        public async Task<IActionResult> Index()
        {
            var recommendations = await _repo.TopProductRecommendations()
                .ToListAsync();

            return View(recommendations);
        }




        //public IActionResult Products()
        //{
        //    var products = _repo.Products.ToListAsync();
        //    return View(products);
        //}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Products()
        {
            // Await the asynchronous operation and obtain the result.
            var products = await _repo.Products.ToListAsync();

            // Pass the result to the view.
            return View(products);
        }

        // Whatever the name of the individual product view is should be put here 
        //public IActionResult ProductDetails(int productID)
        //{
        //    var recommendations = _repo.ProductRecommendations(productID).FirstOrDefault();
        //    return View(recommendations);
        //}

        public IActionResult Cart()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Orders(int pageNum = 1)
        {
            int pageSize = 100; // Set the number of items per page

            var ordersQuery = _repo.Orders.AsQueryable();

            var viewModel = new OrdersListViewModel
            {
                Orders = ordersQuery.Skip((pageNum - 1) * pageSize).Take(pageSize),
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = ordersQuery.Count()  // Make sure to count before pagination
                },
            };

            var orders = _repo.Orders.ToList(); // Assuming '_context' is your database context
            if (orders == null)
            {
                orders = new List<Order>(); // Initialize as empty if null to avoid null reference in the view
            }
            return View(viewModel);
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
            return View(new Order());
        }
        // Post Checkout to database
        [HttpPost]
        public IActionResult Create(Order o)
        {
            if (ModelState.IsValid)
            {
                // Set the date to now
                o.Date = DateOnly.FromDateTime(DateTime.Now);

                // Get the abbreviated day of week, e.g., "Wed" for Wednesday
                o.DayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(DateTime.Now.DayOfWeek);
                o.Time = DateTime.Now.Hour;

                _repo.AddOrder(o);
                return View("Confirmation");
            }
            else
            {
                return View(new Order());
            }
        }

        //Add product
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View(new Product());
        }
        [HttpPost]
        public IActionResult CreateProduct(Product t)
        {
            if (ModelState.IsValid)
            {
                _repo.AddProduct(t);
                return RedirectToAction("Products");
            }
            else
            {
                return View(new Product());
            }
        }

        //Edit product
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _repo.GetProductById(id);
            if (recordToEdit == null)
            {
                return NotFound();
            }

            return View("CreateProduct", recordToEdit);

        }

        [HttpPost]
        public IActionResult Edit(Product updatedProduct)
        {
            _repo.UpdateProduct(updatedProduct); //Good
            return RedirectToAction("Products");
        }

        //product delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _repo.GetProductById(id);
            if (recordToDelete == null)
            {
                return NotFound();
            }
            return View("DeleteConfirmation", recordToDelete); // Pass the record to a new confirmation view
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id) // Renamed to avoid confusion with HttpGet Delete
        {
            var recordToDelete = _repo.GetProductById(id);
            if (recordToDelete != null)
            {
                _repo.RemoveProduct(recordToDelete);
                return RedirectToAction("Products");
            }
            return NotFound();
        }




    }
}
