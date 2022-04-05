using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceLiteBLL.Account;
using ECommerceLiteBLL.Repository;
using ECommerceLiteBLL.Settings;
using ECommerceLiteEntity.IdentityModels;
using ECommerceLiteEntity.Models;
using ECommerceLiteUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ECommerceLiteEntity.Enums;
using System.Threading.Tasks;
using ECommerceLiteEntity.ViewModels;

namespace ECommerceLiteUI.Controllers
{
    public class AccountController : BaseController
    {
        //Global alan
        //Not: Bir sonraki projede repoları UI nin içinde new lemeyeceğiz.
        //Çünkü bu bağımlılık oluşturur!bir sonraki projede bağımlılıkları
        //tersine çevirme işlemi olarak bilinen Dependency Injection işlemleri yapacağız.

        CustomerRepo myCustomerRepo = new CustomerRepo();
        PassiveUserRepo myPassiveUserRepo = new PassiveUserRepo();
        UserManager<ApplicationUser> myUserManager = MembershipTools.NewUserManager();
        UserStore<ApplicationUser> myUserStore = MembershipTools.NewUserStore();
        RoleManager<ApplicationRole> myRoleManager = MembershipTools.NewRoleManager();

        [HttpGet]
        public ActionResult Register()
        {
            //kayıtol sayfası
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//Bot hesaplarını engeller
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)//model validasyonları sağladı mı?
                {
                    return View(model);
                }
                var checkUserTC = myUserStore.Context.Set<Customer>().FirstOrDefault(x => x.TCNumber == model.TCNumber)?.TCNumber;
                if (checkUserTC != null)//Buldu
                {
                    ModelState.AddModelError("", "Bu TC kimlik numarası ile sisteme kayıt yapılmıştır.");
                    return View(model);
                }
                var checkUserEmail = myUserStore.Context.Set<ApplicationUser>().FirstOrDefault(x => x.Email == model.Email)?.Email;
                if (checkUserEmail != null)//Buldu
                {
                    ModelState.AddModelError("", "Bu email ile sisteme kayıt yapılmıştır.");
                    return View(model);
                }

                // artık sisteme kayıt olabilir

                var newUser = new ApplicationUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.TCNumber
                };

                //aktivasyon kodu üretelim

                var activationCode = Guid.NewGuid().ToString().Replace("-", "");
                //ekleyeceğiz

                var createResult = myUserManager.CreateAsync(newUser, model.Password);

                //todo: createResult.Isfault ne acaba
                if (createResult.Result.Succeeded)
                {
                    //görev başarıyla tamamlandıysa kişi aspnetusers tablosuna eklenmiştir.
                    //yeni kayıt olduğu için bu kişiye pasif rol verilecektir.
                    //Kişi emailine gelen aktivasyon koduna tıklarsa pasifiklikten çıkıp customer olabilir.

                    await myUserManager.AddToRoleAsync(newUser.Id, Roles.Passive.ToString());
                    PassiveUser myPassiveUser = new PassiveUser()
                    {
                        UserId = newUser.Id,
                        TCNumber = model.TCNumber,
                        IsDeleted = false,
                        LastActiveTime = DateTime.Now
                    };
                    //  myPassiveUserRepo.Insert(myPassiveUser)
                    await myPassiveUserRepo.InsertAsync(myPassiveUser);
                    //email gönderilecek
                    //site adresi alıyoruz.
                    var siteURL = Request.Url.Scheme + Uri.SchemeDelimiter
                        + Request.Url.Host +
                        (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                    await SiteSettings.SendMail(new MailModel()
                    {
                        To = newUser.Email,
                        Subject = "ECommerceLite Site Aktivasyon Emaili",
                        Message = $"Merhaba {newUser.Name} {newUser.Surname}," +
                        $"<br/>Hesabınızı aktifleştirmek için <b>" +
                        $"<a href='{siteURL}/Account/Activation?" +
                        $"code={activationCode}'>Aktivasyon Linkine</a></b> tıklayınız..."
                    });
                    // işlemler bitti...
                    return RedirectToAction("Login", "Account", new { email = $"{newUser.Email}" });
                }
                else
                {
                    ModelState.AddModelError("", "Kayıt işleminde beklenmedik bir hata oluştu.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw;
                
            }
        }
    }
}