using Microsoft.AspNetCore.Mvc;
using Agenda.Services;
using Agenda.Models;
using Agenda.Models.ViewModels;
using System.Diagnostics;
using Agenda.Services.Exceptions;
using Agenda.Filters;

namespace Agenda.Controllers
{
    [PageUserOn]
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                TempData["MensagemSucesso"] = "Contato criado !";
                await _contactService.CreateContactAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                TempData["MensagemErro"] = $"{ex.Message}. Não foi possível criar o contato";
                return RedirectToAction(nameof(Error), new { Message = ex.Message });
            }
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
            try
            {
                TempData["MensagemSucesso"] = "Contato deletado !";
                await _contactService.RemoveContactAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                TempData["MensagemErro"] = $"{ex.Message}. Não foi possível deletar o contato";
                return RedirectToAction(nameof(Error), new { Message = ex.Message });
            }
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

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                TempData["MensagemSucesso"] = "Contato Editado !";
                await _contactService.UpdateContactAsync(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                TempData["MensagemErro"] = $"{ex.Message}. Não foi possível editar o contato";
                return RedirectToAction(nameof(Error), new { Message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(viewModel);
        }
    }
}
