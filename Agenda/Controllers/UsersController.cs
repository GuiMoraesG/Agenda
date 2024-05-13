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
            await _userService.AddUserAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
