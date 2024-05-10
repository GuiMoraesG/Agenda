using Microsoft.AspNetCore.Mvc;
using Agenda.Services;
using Agenda.Models;
using Agenda.Models.ViewModels;
using System.Diagnostics;

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não recebido" });
            }

            var obj = await _contactService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Usuário não encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.RemoveContactAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não recebido" });
            }

            var obj = await _contactService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Usuário não encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Contact obj)
        {
            if (id != obj.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Usuário não encontrado" });
            }

            await _contactService.UpdateContactAsync(obj);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(viewModel);
        }
    }
}
