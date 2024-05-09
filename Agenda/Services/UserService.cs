using Agenda.Data;
using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Services
{
    public class UserService
    {
        private readonly AgendaContext _context;

        public UserService(AgendaContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.User.ToListAsync();
        }
    }
}
