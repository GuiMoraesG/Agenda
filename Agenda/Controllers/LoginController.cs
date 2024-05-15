using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Agenda.Services;

namespace Agenda.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;

        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var User = await _userService.FindByEmailAsync(user.Email);

                if (User == null)
                {
                    TempData["MensagemErro"] = $"E-mail não cadastrado, por favor criar um novo e-mail";
                    return View();
                }

                if (!User.ValidatePass(user.Password))
                {
                    TempData["MensagemErro"] += $"Senha inválida";
                    return View();
                }

                TempData["MensagemSucesso"] = $"Tudo Certo !!";
                return RedirectToAction(nameof(LoginIn));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"{ex.Message}. Não foi possível realizar o Login";
                return View();
            }
        }
    }
}
