using Agenda.Data;
using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Services
{
    public class ContactService
    {
        private readonly AgendaContext _context;

        public ContactService(AgendaContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            return await _context.Contact.ToListAsync();
        }

        public async Task<Contact> FindByIdAsync(int id)
        {
            return await _context.Contact.Include(obj => obj.User).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task CreateContactAsync(Contact obj)
        {
            obj.User = _context.User.First();
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveContactAsync(int id)
        {
            var obj = await _context.Contact.FindAsync(id);
            _context.Contact.Remove(obj);
            await _context.SaveChangesAsync();
        }
    }
}
