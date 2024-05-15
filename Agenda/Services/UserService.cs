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

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddUserAsync(User user)
        {
            DateTime date = DateTime.Now;
            user.Cdate = date;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            DateTime date = DateTime.Now;
            user.Cdate = date;

            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserAsync(int id)
        {
            var obj = await _context.User.FindAsync(id);

            _context.User.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());
        }
    }
}
