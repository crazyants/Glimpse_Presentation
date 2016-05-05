using System.Web.Mvc;
using Autofac.Extras.NLog;

namespace Kiehl.App.UIMvc.Controllers
{
    public class HomeController : Controller
    {
        public ILogger Logger { get; set; }

        public ActionResult Index()
        {
            Logger?.Trace("Index");

            return View();
        }
    }
}