using Microsoft.AspNetCore.Mvc;

namespace Agenda.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
