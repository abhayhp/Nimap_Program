using Microsoft.AspNetCore.Mvc;
using Nimap_Program.DAL;
using Nimap_Program.Models;
using System;

namespace Nimap_Program.Controllers
{
    public class ProductController : Controller
    {

        
        private readonly IConfiguration configuration;
        ProductDAL productDAL;

        public ProductController(IConfiguration configuration)
        {
            
            this.configuration = configuration;
            productDAL = new ProductDAL(this.configuration);
        }
        public IActionResult Index()
        {

            return View();
        }

        public ActionResult List()
        {

            ViewBag.ProductList = productDAL.GetAllProducts();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Product prod)
        {
            productDAL.AddProduct(prod);
            return RedirectToAction("List");
        }


        public ActionResult Edit(int pid)
        {

            var model = productDAL.GetProductById(pid);
            return View(model);


        }

        [HttpPost]

        public ActionResult Edit(Product prod)
        {

            int result = productDAL.UpdateProduct(prod);
            if (result == 1)
                return RedirectToAction("List");
            else
                return BadRequest();

        }


        public ActionResult Delete(int pid)
        {
            var model = productDAL.GetProductById(pid);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]

        public ActionResult DeleteConfirm(int pid)
        {
            try
            {
                int result = productDAL.DeleteProduct(pid);
                if (result == 1)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        





    }
}
