using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceLiteUI.Controllers
{
    public class ProductController : Controller
    {
        //Bu controller a admin gibi yetkili kişiler erişebilecektir
        //burada ürünlerin listenmesi ekleme,silme, güncelleme işlemleri yapılacaktır.
        public ActionResult ProductList()
        {
            return View();
        }

    }
}