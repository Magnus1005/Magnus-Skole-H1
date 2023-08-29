using Front.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        //public IActionResult Student()
        //{
        //    List<DAL.DalClass.DataList> data = BLL.BllClass.HentAltData();
        //    return View(data);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}