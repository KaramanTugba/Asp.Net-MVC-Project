using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ECommerceLiteBLL.Account;
using ECommerceLiteEntity.Enums;
using Microsoft.AspNet.Identity;
using ECommerceLiteEntity.IdentityModels;

namespace ECommerceLiteUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //NOT: Application_Start
            //uygulama ilk kez çalıştığında bir defaya mahsus olmak üzer çalışır.
            //Bu nedenle ben uyg. ilk kez çalıştığında ds de roller ekli mi diye bakmak istiyorum.
            //ekli değilse rolleri enum dan çağırıp ekleyelim.
            //ekli ise birşey yapmaya gerek yok.

            //adım 1 :rollere bakacağım  -->RoleManager
            var myRoleManager=MembershipTools.NewRoleManager();
            //adım2: rollerin isimlerini almak
            var allRoles = Enum.GetNames(typeof(Roles));
            //adım3: bize gelen diziyi tek tek döneceğiz
            foreach (var item in allRoles)
            {
                //adım4: acaba bu rol db de ekli mi?
                if (!myRoleManager.RoleExists(item))
                {
                    //adım5: rolü ekle.
                    //ApplicationRole role = new ApplicationRole() { Name = item };
                    //myRoleManager.Create(role);
                    myRoleManager.Create(new ApplicationRole() { Name=item});
                }
            }
        }
        protected void Application_Error()
        {
            //NOT: İhtiyaç halinde internetten Global.assax'ın metodlarına bakıp kullanilabilir.
            //ÖRN: Application_Error: Uygulama içinde istenmeyen bir hata meyadan geldiğinde çalışır.
            //Bu metodu kullanarak hata loglanarak çözülebilir.

            Exception ex = Server.GetLastError();
            //ex loglanacak
        }

    }
}
