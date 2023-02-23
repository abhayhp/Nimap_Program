using Microsoft.AspNetCore.Mvc;
using Nimap_Program.DAL;
using Nimap_Program.Models;
using System.Data.SqlClient;

namespace Nimap_Program.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IConfiguration configuration;
        CategoryDAL categoryDAL;

        public CategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
            categoryDAL = new CategoryDAL(this.configuration);
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {

            ViewBag.CategoryList = categoryDAL.GetAllCategories();
            return View();
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Create(Category cat) {
            try
            {
                int result = categoryDAL.AddCategory(cat);
                if (result == 1)
                    return RedirectToAction("List");
                else
                    return BadRequest();

            }
            catch
            {
                return BadRequest();

            }

        }

        public ActionResult Edit(int catid)
        {
            var model = categoryDAL.GetCategoryById(catid);
            return View(model);
        }

        [HttpPost]

        public ActionResult Edit(Category cat)
        {
            try
            {
                int result = categoryDAL.UpdateCategory(cat);
                if (result == 1)
                    return RedirectToAction("List");
                else
                    return BadRequest();

            }
            catch
            {
                return BadRequest();

            }

        }


        public ActionResult Delete(int catid)
        {
            var model = categoryDAL.GetCategoryById(catid);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]

        public ActionResult DeleteConfirm(int catid)
        {
            try
            {
                int result = categoryDAL.DeleteCategory(catid);
                if (result == 1)
                    return RedirectToAction("List");
                else
                    return BadRequest();

            }
            catch
            {
                return BadRequest();

            }


        }


        

    }
}
