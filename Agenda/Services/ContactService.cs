using Agenda.Data;
using Agenda.Models;

namespace Agenda.Services
{
    public class ContactService
    {
        private readonly AgendaContext _context;

        public ContactService(AgendaContext context)
        {
            _context = context;
        }

        public List<Contact> GetContacts()
        {
            return _context.Contact.ToList();
        }
    }
}
