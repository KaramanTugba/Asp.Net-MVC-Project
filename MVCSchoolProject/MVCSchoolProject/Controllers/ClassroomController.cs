using BusinessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSchoolProject.Controllers
{
    public class ClassroomController : Controller
    {
        // GET: Classroom
        public ActionResult Index()
        {
            return View();
        }
        ClassroomManager cm = new ClassroomManager();
        public ActionResult GetClassroomList()
        {
            var classroomValues = cm.GetAll();
            return View(classroomValues);
        }
    }
}