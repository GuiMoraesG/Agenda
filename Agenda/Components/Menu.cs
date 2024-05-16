using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Newtonsoft.Json;

namespace Agenda.Component
{
    [ViewComponent(Name = "Menu")]
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessionUser = HttpContext.Session.GetString("sessionUserOn");

            if (string.IsNullOrEmpty(sessionUser))
            {
                return null;
            }

            User user = JsonConvert.DeserializeObject<User>(sessionUser);

            return View(user);
        }
    }
}
