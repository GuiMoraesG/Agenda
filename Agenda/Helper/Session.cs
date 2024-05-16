using Agenda.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Agenda.Helper
{
    public class Session
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public Session(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public User GetSession()
        {
            string sessionUser = _contextAccessor.HttpContext.Session.GetString("sessionUserOn");

            if (string.IsNullOrEmpty(sessionUser))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<User>(sessionUser);
        }

        public void CreateSession(User user)
        {
            string valor = JsonConvert.SerializeObject(user);
            _contextAccessor.HttpContext.Session.SetString("sessionUserOn", valor);
        }

        public void RemoveSession()
        {
            _contextAccessor.HttpContext.Session.Remove("sessionUserOn");
        }
    }
}
