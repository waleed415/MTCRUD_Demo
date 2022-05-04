using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MTCRUD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MTCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, IAppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            List<Order> orders = new List<Order>();
            orders = _appDbContext.Orders.ToList();
            return View(orders);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Order order = new Order();
            order.Details.Add(new OrderDetail());
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order model)
        {
            model.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                _appDbContext.Orders.Add(model);
                await _appDbContext.SaveChanges();
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order =_appDbContext.Orders.Find(id);
            order.Details = _appDbContext.OrdersDetails.Where(x => x.OrderId == id).ToList();
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Order model)
        {
            var oldModel = _appDbContext.Orders.Find(model.Id);
            oldModel.Details = _appDbContext.OrdersDetails.Where(x => x.OrderId == model.Id).ToList();
            oldModel.Total = model.Total;
            oldModel.CustomerName = model.CustomerName;

            foreach (var item in model.Details)
            {
                if (item.Id == 0)
                {
                    oldModel.Details.Add(item);
                }
                else
                {
                    var oldDet = oldModel.Details.Find(x => x.Id == item.Id);
                    if (oldDet?.Id > 0)
                    {
                        oldDet.ProductName = item.ProductName;
                        oldDet.Price = item.Price;
                    }
                }
            }

           var deletedDet = oldModel.Details.Where(x => !model.Details.Select(y => y.Id).ToList().Contains(x.Id)).ToList();

            foreach (var item in deletedDet)
            {
                
                    _appDbContext.OrdersDetails.Remove(item);
            }
            await _appDbContext.SaveChanges();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
