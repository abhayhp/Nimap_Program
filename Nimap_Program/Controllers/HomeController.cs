using Microsoft.AspNetCore.Mvc;
using Nimap_Program.DAL;
using Nimap_Program.Models;
using System.Diagnostics;

using System.Linq;

namespace Nimap_Program.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration configuration;
        ProductDAL productDAL;

       

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
            productDAL = new ProductDAL(this.configuration);
        }

        public IActionResult Index(int pg = 1)
        {
            List<Product> prods = productDAL.GetAllProducts();

            const int pagesize = 3;
            if(pg < 1)
            {
                pg = 1;
            }

            int recscount = prods.Count();

            var pager = new Pager(recscount, pg, pagesize);

            int recskip = (pg - 1) * pagesize;

            var data = prods.Skip(recskip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
                       

            return View(data);
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