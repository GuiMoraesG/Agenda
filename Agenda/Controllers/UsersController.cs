using Microsoft.AspNetCore.Mvc;

namespace Agenda.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
