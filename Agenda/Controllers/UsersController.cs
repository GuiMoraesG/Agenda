using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Agenda.Services;

namespace Agenda.Controllers
{
    public class UsersController : Controller
    {
        public readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _userService.GetUsersAsync();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                TempData["MensagemSucesso"] = "Conta cadastrado com sucesso";
                await _userService.AddUserAsync(user);
                return Redirect("https://localhost:7050/Login/Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Não foi possível criar sua conta, {ex.Message}";
                return Redirect("https://localhost:7050/Login/Index");
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var obj = await _userService.FindByIdAsync(id.Value);

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                TempData["MensagemSucesso"] = "Conta editado com sucesso";
                await _userService.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Não foi possível editar sua conta, {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var obj = await _userService.FindByIdAsync(id.Value);

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                TempData["MensagemSucesso"] = "Conta deletada com sucesso";
                await _userService.RemoveUserAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Não foi possível deletar sua conta, {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
