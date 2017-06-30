using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using SiteCoreTrainings.Data.Models;
using SiteCoreTrainings.Infrastructure.Models;

namespace SiteCoreTrainings.Controllers
{
    public class HomePageController : BaseController
    {
        public ActionResult HomePage()
        {
            //var item = ContextItem;
            //MultilistField authors = item.Fields["Authors"];
            //authors.TargetIDs

            //YoutubeFiled authors = item.Fields["Authors"];
            //authors.Get


            var context = SitecoreContext.GetCurrentItem<HomePage>();
            return View(context);
        }
    }
}