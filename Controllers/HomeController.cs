using MyGames.Service;
using System.Web.Mvc;
namespace MyGames.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public JsonResult GetDateTime()
        {
            return Json(GameService.GetDateTime().ToString("yyyy-MM-ddTHH:mm:ss"), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllGames()
        {
            return Json(GameService.GetGames(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult PlayGame(int gameID, int modeID, string userMove = "")
        {
            return Json(GameService.PlayGame(gameID, modeID, userMove), JsonRequestBehavior.AllowGet);
        }

    }
}