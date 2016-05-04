using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace c3o.Logger.Test.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            throw new DivideByZeroException("boo");
            throw new Exception("test2");
            throw new Exception("test1");


            return View();
        }
    }
}