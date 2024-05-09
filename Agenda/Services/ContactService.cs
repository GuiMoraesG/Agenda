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

        public async Task CreateContactAsync(Contact obj)
        {
            obj.User = _context.User.First();
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
    }
}
