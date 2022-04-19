using ECommerceLiteBLL.Repository;
using ECommerceLiteUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceLiteUI.Controllers
{
    public class HomeController : Controller
    {
        CategoryRepo myCategoryRepo = new CategoryRepo();
        ProductRepo myProductRepo = new ProductRepo();
        public ActionResult Index()
        {
            // ANA KATEGORİLERİ VİEWBAG İLE SAYFAYA GÖNDERELİM

            var categoryList = myCategoryRepo.AsQueryable().Where(x => x.BaseCategoryId == null).Take(4).ToList();
            ViewBag.CategoryList = categoryList.OrderByDescending(x => x.Id);
            //
            var productList = myProductRepo.AsQueryable().Where(x => !x.IsDeleted && x.Quantity >= 1).Take(10).ToList();
            List<ProductViewModel> model = new List<ProductViewModel>();
            foreach (var item in productList)
            {
                var product = new ProductViewModel()
                {
                    Id = item.Id,
                    CategoryId=item.CategoryId,
                    ProductName=item.ProductName,
                    Description=item.Description,
                    Quantity=item.Quantity,
                    Discount=item.Discount,
                    RegisterDate=item.RegisterDate,
                    Price=item.Price,
                    ProductCode=item.ProductCode
                    //isdeleted alanı view modelin içerisine eklemedik
                };
                product.GetCategory();
                product.GetProductPictures();
                model.Add(product);
               
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}