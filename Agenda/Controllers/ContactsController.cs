using Microsoft.AspNetCore.Mvc;
using Agenda.Services;
using Agenda.Models;

namespace Agenda.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactService _contactService;

        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _contactService.GetContactsAsync();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            Console.Write(contact);
            await _contactService.CreateContactAsync(contact);
            return RedirectToAction(nameof(Index));
        }
    }
}
