using System.Web.Mvc;
using SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages;

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


            var context = SitecoreContext.GetCurrentItem<Home>();
            return View(context);
        }
    }
}