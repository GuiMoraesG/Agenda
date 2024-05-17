using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Agenda.Services;
using Agenda.Helper;
using Agenda.Filters;

namespace Agenda.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;
        private readonly Session _session;

        public LoginController(UserService userService, Session session)
        {
            _userService = userService;
            _session = session;
        }

        public IActionResult Index()
        {
            if (_session.GetSession() == null)
            {
                return View();
            }

            return RedirectToAction(nameof(LoginIn));
        }

        [PageUserOn]
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

                _session.CreateSession(User);
                TempData["MensagemSucesso"] = $"Tudo Certo !!";
                return RedirectToAction(nameof(LoginIn));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"{ex.Message}. Não foi possível realizar o Login";
                return View();
            }
        }

        public IActionResult LogOut()
        {
            _session.RemoveSession();

            return RedirectToAction(nameof(Index));
        }
    }
}
