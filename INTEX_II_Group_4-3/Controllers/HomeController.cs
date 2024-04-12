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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTEX_II_Group_4_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILegoRepository _repo;
        private readonly InferenceSession _session;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public readonly string _onnxModelPath;


        public HomeController(ILegoRepository temp, ILogger<HomeController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IHostEnvironment hostEnvironment)
        {
            _repo = temp;
            _onnxModelPath = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "FraudDetection.onnx");
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;

                try 
                {
                    _session = new InferenceSession(_onnxModelPath);
                    _logger.LogInformation("ONNX loaded successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to load ONNX model: {ex.Message}");
                }
            }

            // Render the Checkout View
            [HttpGet]
            public IActionResult Checkout()
            {
                return View();
                //return View(new Order());
            }

        [HttpPost]
        public IActionResult Checkout(FraudPrediction Fraud)
        {
            // Dictionary mapping the numeric prediction to a fraud classification
            var class_type_dict = new Dictionary<int, string>
    {
        { 0, "Not Fraud" },
        { 1, "Fraud" }
    };

            float Time = (float)Fraud.Order.Time;
            float Amount = (float)Fraud.Order.Amount;
            float Year = DateTime.Now.Year;
            float Month = DateTime.Now.Month;
            float Day = DateTime.Now.Day;

            float country_of_transaction_UK = Fraud.Order.CountryOfTransaction.Equals("United Kingdom", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float country_of_transaction_India = Fraud.Order.CountryOfTransaction.Equals("India", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float country_of_transaction_Russia = Fraud.Order.CountryOfTransaction.Equals("Russia", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float country_of_transaction_USA = Fraud.Order.CountryOfTransaction.Equals("USA", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;

            float entry_mode_PIN = Fraud.Order.EntryMode.Equals("PIN", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float entry_mode_Tap = Fraud.Order.EntryMode.Equals("Tap", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;

            float type_of_transaction_Online = Fraud.Order.TypeOfTransaction.Equals("Online", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float type_of_transaction_POS = Fraud.Order.TypeOfTransaction.Equals("POS", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;

            float shipping_address_India = Fraud.Order.ShippingAddress.Equals("India", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float shipping_address_Russia = Fraud.Order.ShippingAddress.Equals("Russia", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float shipping_address_USA = Fraud.Order.ShippingAddress.Equals("USA", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float shipping_address_UK = Fraud.Order.ShippingAddress.Equals("United Kingdom", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;

            float bank_HSBC = Fraud.Order.Bank.Equals("HSBC", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float bank_Halifax = Fraud.Order.Bank.Equals("Halifax", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float bank_Lloyds = Fraud.Order.Bank.Equals("Lloyds", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float bank_Metro = Fraud.Order.Bank.Equals("Metro", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float bank_Monzo = Fraud.Order.Bank.Equals("Monzo", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float bank_RBS = Fraud.Order.Bank.Equals("RBS", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;

            DateTime currentDate = DateTime.Now;
            string dayOfWeekString = currentDate.ToString("ddd"); // This will give you "Mon", "Tue", etc.

            float day_of_week_Mon = dayOfWeekString.Equals("Mon", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float day_of_week_Tue = dayOfWeekString.Equals("Tue", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float day_of_week_Wed = dayOfWeekString.Equals("Wed", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float day_of_week_Thu = dayOfWeekString.Equals("Thu", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float day_of_week_Sat = dayOfWeekString.Equals("Sat", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;
            float day_of_week_Sun = dayOfWeekString.Equals("Sun", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;

            float type_of_card_Visa = Fraud.Order.EntryMode.Equals("Visa", StringComparison.OrdinalIgnoreCase) ? 1f : 0f;

            try
            {
                var input = new List<float>
        {
            Time, Amount, Year, Month, Day, day_of_week_Mon, day_of_week_Sat, day_of_week_Sun, day_of_week_Thu,
            day_of_week_Tue, day_of_week_Wed, entry_mode_PIN, entry_mode_Tap, type_of_transaction_Online,
            type_of_transaction_POS, country_of_transaction_India, country_of_transaction_Russia,
            country_of_transaction_USA, country_of_transaction_UK, shipping_address_India,
            shipping_address_Russia, shipping_address_USA, shipping_address_UK, bank_HSBC,
            bank_Halifax, bank_Lloyds, bank_Metro, bank_Monzo, bank_RBS, type_of_card_Visa
        };
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

                var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
        };

                using (var results = _session.Run(inputs)) // makes the prediction with the inputs from the form
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    if (prediction != null && prediction.Length > 0)
                    {
                        var fraudClassify = class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown");

                        if (fraudClassify == "Fraud")
                        {
                            ViewBag.Prediction = "Transaction is classified as Fraud.";
                            return View("ConfirmationFraud");
                        }
                        else
                        {
                            ViewBag.Prediction = "Transaction is classified as Not Fraud.";
                            return View("ConfirmationOK");
                        }
                    }
                    else
                    {
                        ViewBag.Prediction = "Error: Unable to make a prediction.";
                        return View("Error"); // Ensure you have an Error view to handle this case
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during prediction: {ex.Message}");
                ViewBag.Prediction = "Error during prediction.";
                return View("Error"); // Ensure you have an Error view to handle exceptions
            }
        }

        public IActionResult ShowPredictions()
            {
                var records = _repo.Orders
                //.OrderByDescending(o=>o.Date)
                .Take(20).ToList();  // Fetch all records
                var predictions = new List<FraudPrediction>();  // Your ViewModel for the view

                // Dictionary mapping the numeric prediction to a fraud classification
                var class_type_dict = new Dictionary<int, string>
            {
                { 0, "Not Fraud" },
                { 1, "Fraud" }
            };

                foreach (var record in records)
                {
                //var january1_2022 = new DateTime(2022, 1, 1);
                //var daysSinceJan12022 = record.Date.HasValue ? Math.Abs((record.Date.Value - january1_2022).Days) : 0;

                var input = new List<float>
                {
                    (float)record.Time,
                    (float)record.Amount,

                    record.DayOfWeek == "Mon" ? 1:0,
                    record.DayOfWeek == "Tue" ? 1:0,
                    record.DayOfWeek == "Wed" ? 1:0,
                    record.DayOfWeek == "Thu" ? 1:0,
                    record.DayOfWeek == "Fri" ? 1:0,
                    record.DayOfWeek == "Sat" ? 1:0,
                    record.DayOfWeek == "Sun" ? 1:0,

                    record.EntryMode == "PIN" ? 1:0,
                    record.EntryMode == "Tap" ? 1:0,

                    record.TypeOfTransaction == "Online" ? 1:0,
                    record.TypeOfTransaction == "POS" ? 1:0,

                    record.CountryOfTransaction == "India" ? 1:0,
                    record.CountryOfTransaction == "Russia" ? 1:0,
                    record.CountryOfTransaction == "USA" ? 1:0,
                    record.CountryOfTransaction == "United Kingdom" ? 1:0,

                    record.ShippingAddress == "India" ? 1:0,
                    record.ShippingAddress == "Russia" ? 1:0,
                    record.ShippingAddress == "USA" ? 1:0,
                    record.ShippingAddress == "United Kingdom" ? 1:0,

                    record.Bank == "RBS" ? 1:0,
                    record.Bank == "Lloyds" ? 1:0,
                    record.Bank == "Barclays" ? 1:0,
                    record.Bank == "Halifax" ? 1:0,
                    record.Bank == "Monzo" ? 1:0,
                    record.Bank == "HSBC" ? 1:0,
                    record.Bank == "Metro" ? 1:0,

                    record.TypeOfCard == "MasterCard" ? 1:0,
                    record.TypeOfCard == "Visa" ? 1:0,
                };
                    var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

                    var inputs = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                };

                    string predictionResult;
                    using (var results = _session.Run(inputs))
                    {
                        var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                        predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error in prediction";
                    }

                    predictions.Add(new FraudPrediction { Order = record, Prediction = predictionResult }); // Adds the order information and prediction for that order to FraudPrediction viewmodel
                }

                return View(predictions);
            }


            public IActionResult Shop(int pageNum, string? productCategory, string? productColor, int pageSize = 10)
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


        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError(string.Empty, error.Description);
        //    }
        //}

 


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
                int pageSize = 20;

                // User 6 Query 
                var UserQuery = _repo.Products
                    .Join(_repo.UserProductRecommendations,
                          product => product.ProductId,
                          UserRec => UserRec.ProductId,
                          (product, UserRec) => new { product, UserRec })
                    .Select(joinedItem => new UserProductRecommendation
                    {
                        ProductId = joinedItem.product.ProductId,
                        Name = joinedItem.product.Name,
                        Year = joinedItem.product.Year,
                        NumParts = joinedItem.product.NumParts,
                        Price = joinedItem.product.Price,
                        ImgLink = joinedItem.product.ImgLink,
                        PrimaryColor = joinedItem.product.PrimaryColor,
                        SecondaryColor = joinedItem.product.SecondaryColor,
                        Description = joinedItem.product.Description,
                        Category = joinedItem.product.Category
                    });

                var UserRec = UserQuery
                    .Take(pageSize)
                    .ToList(); // Materialize the query to execute it

                // Top 20 Query 
                var query = _repo.Products
                    .Join(_repo.TopProductRecommendations,
                          product => product.ProductId,
                          top_20_product => top_20_product.ProductId,
                          (product, top_20_product) => new { product, top_20_product })
                    .Select(joinedItem => new TopProductRecommendation
                    {
                        ProductId = joinedItem.product.ProductId,
                        Name = joinedItem.product.Name,
                        Year = joinedItem.product.Year,
                        NumParts = joinedItem.product.NumParts,
                        Price = joinedItem.product.Price,
                        ImgLink = joinedItem.product.ImgLink,
                        PrimaryColor = joinedItem.product.PrimaryColor,
                        SecondaryColor = joinedItem.product.SecondaryColor,
                        Description = joinedItem.product.Description,
                        Category = joinedItem.product.Category
                    });


                var products = query
                    .Take(pageSize)
                    .ToList(); // Materialize the query to execute it

                var setup = new RecommendationListViewModel
                {
                    TopProductRecommendations = products,
                    UserProductRecommendations = UserRec

                };

                // Pass the viewModel to the "ProductDisplay" view
                return View(setup);
            
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();
            var userRoles = new Dictionary<string, string>();
            foreach (var user in users)
            {
                var roleList = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roleList.FirstOrDefault() ?? "No role";
            }

            var viewModel = new UserManagementViewModel
            {
                Users = users,
                UserRoles = userRoles,
                Roles = roles
            };

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users(string userId, string newRole, string command)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(); // Return to the same view with an error message
            }

            if (command == "EditRole" && !string.IsNullOrEmpty(newRole))
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to remove old roles");
                    return View(); // Show error message
                }

                var addRoleResult = await _userManager.AddToRoleAsync(user, newRole);
                if (!addRoleResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to add new role");
                    return View(); // Show error message
                }
            }
            else if (command == "Delete")
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to delete user");
                    return View(); // Show error message
                }
            }

            return RedirectToAction("Users");
        }

    }
}







