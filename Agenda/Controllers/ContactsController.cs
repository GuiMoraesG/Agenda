using Microsoft.AspNetCore.Mvc;
using Agenda.Services;

namespace Agenda.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactService _contactService;

        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            var list = _contactService.GetContacts();

            return View(list);
        }
    }
}
