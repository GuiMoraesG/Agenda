using Agenda.Data;
using Agenda.Models;
using Microsoft.EntityFrameworkCore;
using Agenda.Services.Exceptions;
using Agenda.Helper;

namespace Agenda.Services
{
    public class ContactService
    {
        private readonly AgendaContext _context;
        private readonly Session _session;
        private readonly UserService _userService;

        public ContactService(AgendaContext context, Session session, UserService userService)
        {
            _context = context;
            _session = session;
            _userService = userService;
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            var userOn = _session.GetSession();

            return await _context.Contact.Where(x => x.User == userOn).ToListAsync();
        }

        public async Task<Contact> FindByIdAsync(int id)
        {
            return await _context.Contact.Include(obj => obj.User).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task CreateContactAsync(Contact obj)
        {
            var userOn = _session.GetSession();
            User user = await _userService.FindByIdAsync(userOn.Id);
            obj.User = user;
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveContactAsync(int id)
        {
            try
            {
                var obj = await _context.Contact.FindAsync(id);
                _context.Contact.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task UpdateContactAsync(Contact obj)
        {
            bool hasAny = await _context.Contact.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new IntegrityException("Id not Found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message);
            }
        }
    }
}
