using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpiritOfLifeSanctuary.Controllers
{
    public class NavigateController : Controller
    {
        //
        // GET: /Navigate/

        public ActionResult Home()
        {
            return View("~/Views/Home.cshtml");
        }

        public ActionResult Calendar()
        {
            return View("~/Views/Calendar.cshtml");
        }
        
        public ActionResult Contact()
        {
            return View("~/Views/Contact.cshtml");
        }

        public ActionResult Workshops()
        {
            return View("~/Views/Workshops.cshtml");
        }

        public ActionResult ChurchService()
        {
            return View("~/Views/ChurchService.cshtml");
        }

        public ActionResult Volunteers()
        {
            return View("~/Views/Volunteers.cshtml");
        }
    
        public ActionResult Event( string id )
        {
            return View( "~/Views/Event.cshtml", FacebookController.Event(id) );
        }

    }
}
