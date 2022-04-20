using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceLiteBLL.Repository;
using ECommerceLiteUI.Models;
using Mapster;

namespace ECommerceLiteUI.Controllers
{
    public class HomeController : Controller
    {
        CategoryRepo myCategoryRepo = new CategoryRepo();
        ProductRepo myProductRepo = new ProductRepo();

        public ActionResult Index()
        {
            // Ana kategorilerden 4 tanesini viewbag ile sayfaya gönderelim
            var categoryList = myCategoryRepo.AsQueryable()
                .Where(x => x.BaseCategoryId == null).Take(4).ToList();

            ViewBag.CategoryList = categoryList.OrderByDescending(x => x.Id).ToList();

            //ürünler
            var productList = myProductRepo.AsQueryable()
                .Where(x => !x.IsDeleted && x.Quantity >= 1).Take(10).ToList();
            List<ProductViewModel> model = new List<ProductViewModel>();

            //foreach (var item in productList)
            //{

            //    var product = new ProductViewModel()
            //    {
            //        Id = item.Id,
            //        CategoryId = item.CategoryId,
            //        ProductName = item.ProductName,
            //        Description = item.Description,
            //        Quantity = item.Quantity,
            //        Discount = item.Discount,
            //        RegisterDate = item.RegisterDate,
            //        Price = item.Price,
            //        ProductCode = item.ProductCode
            //        //isDeleted alanını viewmodelin içine eklemeyi unuttuk. Çünkü
            //        // isDeleted alanını daha dün ekledik. Viewmodeli geçen hafta oluşturduk
            //    };
            //    product.GetCategory();
            //    product.GetProductPictures();
            //    model.Add(product);
            //}
            //Mapster ile mapledik
            productList.ForEach(x => {
                var item = x.Adapt<ProductViewModel>();
                item.GetCategory();
                item.GetProductPictures();
                model.Add(item);
            });
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
        public ActionResult AddToCard(int id)
        {
            try
            {
                //session eklenecek
                //session oturum demektir
                var shoppingCard = Session["ShoppingCard"] as List<ProductViewModel>;

                if (shoppingCard==null)
                {
                    shoppingCard = new List<ProductViewModel>();
                }
                if (id>0)
                {
                    var product = myProductRepo.GetById(id);
                    if (product==null)
                    {
                        //ex loglanacak. product null geldi.
                        TempData["AddToCardFailed"] = "ürün eklemesi başarısız oldu.Lütfen tekrar deneyiniz";
                        return RedirectToAction("Index", "Home");
                    }
                    //tamam ekleme yapılacak.
                    //ProductViewModel productAddToCard=product.Adapt<ProductViewModel>();
                    var productAddToCard = new ProductViewModel()
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        Description = product.Description,
                        CategoryId = product.CategoryId,
                        Discount = product.Discount,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        RegisterDate = product.RegisterDate,
                        ProductCode = product.ProductCode
                    };
                    if (shoppingCard.Count(x=>x.Id==productAddToCard.Id)>0)
                    {
                        shoppingCard.FirstOrDefault(x => x.Id == productAddToCard.Id).Quantity++;
                    }
                    else
                    {
                        productAddToCard.Quantity = 1;
                        shoppingCard.Add(productAddToCard);

                    }
                    //Önemli     
                    //Session a bu liste atanmalı.
                    Session["ShoppingCard"] = shoppingCard;
                    TempData["AddToCardSucsess"] = "Ürün sepete eklendi";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["AddToCardFailed"] = "ürün eklemesi başarısız oldu.Lütfen tekrar deneyiniz";
                    //ex loglama yapılacak id düzgün gelmedi
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {

                //ex loglanacak
                TempData["AddToCardFailed"] = "ürün eklemesi başarısız oldu.Lütfen tekrar deneyiniz";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}