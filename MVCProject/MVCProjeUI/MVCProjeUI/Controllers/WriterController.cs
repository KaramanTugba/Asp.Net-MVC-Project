using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeUI.Controllers
{
    public class WriterController : Controller
    {
        // GET: Writer
        public ActionResult Index()
        {
            return View();
        }
        WriterManager cm = new WriterManager();

        public ActionResult GetWriterList()
        {
            var writervalues = cm.GetAll();
            return View(writervalues);
        }
        [HttpGet] //sayfa ilk yuklendiğinde alttaki metot çalışacak
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost] // Butona tıklandıgında alttaki metot çalışcak
        public ActionResult AddWriter(Writer p)
        {
            cm.WriterAddBL(p);
            return RedirectToAction("GetWriterList");
        }
    }
}