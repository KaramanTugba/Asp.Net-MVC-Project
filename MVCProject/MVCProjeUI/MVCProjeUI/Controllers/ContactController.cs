using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeUI.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        ContactManager cm = new ContactManager();

        public ActionResult GetContactList()
        {
            var contactvalues = cm.GetAll();
            return View(contactvalues);
        }
        [HttpGet] //sayfa ilk yuklendiğinde alttaki metot çalışacak
        public ActionResult AddContact()
        {
            return View();
        }
        [HttpPost] // Butona tıklandıgında alttaki metot çalışcak
        public ActionResult AddContact(Contact p)
        {
            cm.ContactAddBL(p);
            return RedirectToAction("GetContactList");
        }

    }
}