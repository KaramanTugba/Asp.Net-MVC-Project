using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceLiteBLL.Repository;

namespace ECommerceLiteUI.Controllers
{
    public class ProductController : Controller
    {
        CategoryRepo myCategoryRepo = new CategoryRepo();//crud işlemleri için repo çağırmalıyız


        //Bu controllera Admin gibi yetkili kişiler erişecektir
        // Burada ürünlerin listelenmesi, ekleme, silme, güncelleme işlemleri yapılacaktır.
        public ActionResult ProductList()
        {s
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
           
        //sayfayı çağırırken ürün kategorisinin ne olduğunu seçmesi lazım bu nedenle sayfaya kategoriler gitmeli
        List<SelectListItem> subCategories = new List<SelectListItem>();
            //Linq
            //select * from Categories where BaseCategoryId is not null

            myCategoryRepo.AsQueryable().Where(x => x.BaseCategoryId != null).ToList().ForEach(x => subCategories.Add(new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }));
            ViewBag.SubCategories = subCategories;
 
            return View();
        }
    }
}