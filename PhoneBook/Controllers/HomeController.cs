using PhoneBookNamespace.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookNamespace.Controllers
{
    public class HomeController : Controller
    {        
        
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Phonebooks");
        }
       
    }
}