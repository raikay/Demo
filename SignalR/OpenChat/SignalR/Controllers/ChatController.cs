using System.Web.Mvc;

namespace SignalR.Controllers
{
    public class ChatController : Controller
    {
        //
        // GET: /Chat/
        public ActionResult Open()
        {
            return View();
        }
	}
}