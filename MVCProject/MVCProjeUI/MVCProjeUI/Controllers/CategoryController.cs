using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeUI.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        CategoryManager cm = new CategoryManager();

        public ActionResult GetCategoryList()
        {
            var categoryvalues = cm.GetAll();
            return View(categoryvalues);
        }
        [HttpGet] //sayfa ilk yuklendiğinde alttaki metot çalışacak
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost] // Butona tıklandıgında alttaki metot çalışcak
        public ActionResult AddCategory(Category p)
        {
            cm.CategoryAddBL(p);
            return RedirectToAction("GetCategoryList");
        }

    }
}