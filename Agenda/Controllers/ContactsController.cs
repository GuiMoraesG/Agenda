using Microsoft.AspNetCore.Mvc;

namespace Agenda.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
