using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceLiteBLL.Repository;
using ECommerceLiteBLL.Settings;
using ECommerceLiteEntity.Models;
using ECommerceLiteUI.Models;
using Mapster;

namespace ECommerceLiteUI.Controllers
{
    public class ProductController : Controller
    {
        CategoryRepo myCategoryRepo = new CategoryRepo();
        ProductRepo myProductRepo = new ProductRepo();
        ProductPictureRepo myProductPictureRepo = new ProductPictureRepo();

        //Bu controllera Admin gibi yetkili kişiler erişecektir
        // Burada ürünlerin listelenmesi, ekleme, silme, güncelleme işlemleri yapılacaktır.
        public ActionResult ProductList(string search = "")
        {
            List<Product> allProducts = new List<Product>();
            //var allProducts = myProductRepo.GetAll();
            if (string.IsNullOrEmpty(search))
            {
                allProducts = myProductRepo.GetAll();
            }
            else
            {
                allProducts = myProductRepo.GetAll()
                    .Where(x => x.ProductName.ToLower().Contains(search.ToLower())
                    || x.Description.ToLower().Contains(search.ToLower())).ToList();
            }
           return View(allProducts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Sayfayı çağırırken ürünün kategorisinin ne olduğu seçmesi lazım
            //Bu nedenle sayfaya kategoriler gitmeli
            List<SelectListItem> subCategories = new List<SelectListItem>();
            //Linq
            //select * from Categories where BaseCategoryId is not null

            myCategoryRepo.AsQueryable().Where(x => x.BaseCategoryId != null)
                .ToList().ForEach(x => subCategories.Add(
                    new SelectListItem()
                    {
                        Text = x.CategoryName,
                        Value = x.Id.ToString()
                    }));

            ViewBag.SubCategories = subCategories;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            try
            {
                List<SelectListItem> subCategories = new List<SelectListItem>();
                myCategoryRepo.AsQueryable().Where(x => x.BaseCategoryId != null)
                    .ToList().ForEach(x => subCategories.Add(
                        new SelectListItem()
                        {
                            Text = x.CategoryName,
                            Value = x.Id.ToString()
                        }));

                ViewBag.SubCategories = subCategories;

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Veri girişleri düzgün olmalıdır");
                    return View(model);
                }

                if (model.CategoryId <= 0 ||
                    model.CategoryId > myCategoryRepo.GetAll().Count())
                {
                    ModelState.AddModelError("", "Ürüne ait kategori seçilmelidir!");
                    return View(model);
                }

                // Burada kontrol lazım
                // Acaba girdiği ürün kodu bizim db zaten var mı?

                if (myProductRepo.IsSameProductCode(model.ProductCode))
                {
                    ModelState.AddModelError("", "Dikkat! Girdiğiniz ürün kodu sistemdeki bir başka ürüne aittir. Ürün kodları benzersiz olmalıdır");
                    return View(model);
                }

                //Ürün tabloya kayıt olacak.
                //TODO: Mapleme yapılacak

                //Product product = new Product()
                //{
                //    ProductName = model.ProductName,
                //    Description = model.Description,
                //    ProductCode = model.ProductCode,
                //    CategoryId = model.CategoryId,
                //    Discount = model.Discount,
                //    Quantity = model.Quantity,
                //    RegisterDate = DateTime.Now,
                //    Price = model.Price
                //};

                //mapleme yapıldı.
                //Mapster paketi indirildi. Mapster bir objedeki verilri diğer bir objeye zahmetsizce aktarır.
                //Aktarım yapabilmesi için A objesiyle B objesinin içindeki propertylerin 
                //isimleri ve tipleri birebir aynı olmalıdır.
                //Bu projede Mapster kullandık
                //Core projesinde daha profesyonel olan AutoMapper'ı kullanacağız
                // Bir dto objesinin içindeki verileri alır asıl objenin içine aktarır. Asıl objenin verilerini dto objesinin içindeki propertylere aktarır.
                Product product = model.Adapt<Product>();
                //Product product2 = model.Adapt<ProductViewModel,Product>();

                int insertResult = myProductRepo.Insert(product);
                if (insertResult > 0)
                {
                    // Sıfırdan büyükse product tabloya eklendi
                    // Acaba bu producta resim seçmiş mi? resim seçtiyse o
                    // resimlerin yollarını kayıt et
                    if (model.Files.Any())
                    {
                        ProductPicture productPicture = new ProductPicture();
                        productPicture.ProductId = product.Id;
                        productPicture.RegisterDate = DateTime.Now;
                        int counter = 1; // Bizim sistemde resim adeti 5
                                         // olarak belirlendiği için

                        foreach (var item in model.Files)
                        {
                            if (counter == 5) break;

                            if (item != null && item.ContentType.Contains("image") &&
                                item.ContentLength > 0)
                            {
                                string filename =
                                    SiteSettings
                                    .StringCharacterConverter(model.ProductName).
                                    ToLower().Replace("-", "");
                                string extensionName = Path.GetExtension(item.FileName);
                                string directoryPath =
                                    Server.MapPath($"~/ProductPictures/{filename}/{model.ProductCode}");
                                string guid = Guid.NewGuid().ToString().Replace("-", "");
                                string filePath =
                                    Server.MapPath($"~/ProductPictures/{filename}/{model.ProductCode}/")
                                    + filename + "-" + counter + "-" + guid + extensionName;
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                item.SaveAs(filePath);
                                //TODO : Buraya birisi çeki düzen verse süper olur
                                if (counter == 1)
                                {
                                    productPicture.ProductPicture1 =
                                       $"/ProductPictures/{filename}/{model.ProductCode}/"
                                    + filename + "-" + counter + "-" + guid + extensionName;
                                }
                                if (counter == 2)
                                {
                                    productPicture.ProductPicture2 =
                                       $"/ProductPictures/{filename}/{model.ProductCode}/"
                                    + filename + "-" + counter + "-" + guid + extensionName;
                                }
                                if (counter == 3)
                                {
                                    productPicture.ProductPicture3 =
                                       $"/ProductPictures/{filename}/{model.ProductCode}/"
                                    + filename + "-" + counter + "-" + guid + extensionName;
                                }
                            }
                            counter++;
                        }
                        //TO DO: Yukarıyı for'a dönüştürebilir miyiz?
                        //for (int i = 0; i < model.Files.Count; i++)
                        //{
                        //}
                        int productPictureInsertResult =
                            myProductPictureRepo.Insert(productPicture);
                        if (productPictureInsertResult > 0)
                        {
                            return RedirectToAction("ProductList", "Product");
                        }
                        else
                        {
                            ModelState.AddModelError("",
                                "Ürün eklendi ama ürüne ait fotoğraf(lar) eklenirken beklenmedik bir hata oluştu! Ürününüzün fotoğraflarını daha sonra tekrar eklemeyi deneyebilirsiniz...");
                            return View(model);
                        }


                    }
                    else
                    {
                        return RedirectToAction("ProductList", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("",
                        "HATA: Ürün ekleme işleminde bir hata oluştu! Tekrar deneyiniz!");
                    return View(model);
                }




            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmekdik bir hata oluştu");
                //ex loglanacak
                return View(model);
            }
        }

    }
}