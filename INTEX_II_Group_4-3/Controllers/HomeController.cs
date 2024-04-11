using INTEX_II_Group_4_3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using INTEX_II_Group_4_3.Models.ViewModels;
using Microsoft.ML.OnnxRuntime;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.CodeAnalysis.Options;

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
                _session = new InferenceSession(@"C:\Users\malea\source\repos\INTEX_II_Group_4-3\INTEX_II_Group_4-3\FraudDetection.onnx");
                _logger.LogInformation("ONNX loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to load ONNX model: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Predict(int TransactionId, int CustomerId, int Time, int Amount, int Age, int DayOfWeek, int EntryMode,int TypeOfTransaction,int CountryOfTransaction, int ShippingAddress, int Bank, int TypeOfCard, int Fraud)
        {
            // Dictionary mapping the numeric prediction to a fraud classification
            var class_type_dict = new Dictionary<int, string>
            {
                { 0, "Not Fraud" },
                { 1, "Fraud" }
            };

            try
            {
                var input = new List<float> {TransactionId,  CustomerId,  Time,  Amount,  Age,  DayOfWeek,  EntryMode,  TypeOfTransaction,  CountryOfTransaction,  ShippingAddress,  Bank,  TypeOfCard,  Fraud};
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

                var inputs = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                };

                using (var results = _session.Run(inputs)) // makes the prediction with the inputs from the form (i.e. class_type 1-7)
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    if (prediction != null && prediction.Length > 0)
                    {
                        // Use the prediction to get the fraud or not from the dictionary
                        var fraudClassify = class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown");
                        ViewBag.Prediction = fraudClassify;
                    }
                    else
                    {
                        ViewBag.Prediction = "Error: Unable to make a prediction.";
                    }
                }

                _logger.LogInformation("Prediction executed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during prediction: {ex.Message}");
                ViewBag.Prediction = "Error during prediction.";
            }

            return View("Index");
        }

        public IActionResult ShowPredictions()
        {
            var records = _repo.Orders.ToList();  // Fetch all records
            var predictions = new List<FraudPrediction>();  // Your ViewModel for the view

            // Dictionary mapping the numeric prediction to a fraud classification
            var class_type_dict = new Dictionary<int, string>
            {
                { 0, "Not Fraud" },
                { 1, "Fraud" }
            };

            foreach (var record in records)
            {
                var input = new List<float>
                {
                    (float)record.TransactionId,
                    (float)record.CustomerId,
                    (float)record.Time,
                    (float)(record.Amount),
                    (float)record.Time,

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

        public async Task<IActionResult> ProductDetail(int id)
        {
            var productData = await _repo.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            return View(productData);
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

        //public IActionResult Products()
        //{
        //    var products = _repo.Products.ToListAsync();
        //    return View(products);
        //}

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
    }
}
