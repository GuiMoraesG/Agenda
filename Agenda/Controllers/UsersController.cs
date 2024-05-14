﻿using Microsoft.AspNetCore.Mvc;
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

                TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                await _userService.AddUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Não foi possível cadastrar seu contato, {ex.Message}";
                return RedirectToAction(nameof(Index));
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _userService.UpdateUserAsync(user);

            return RedirectToAction(nameof(Index));
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
            await _userService.RemoveUserAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
