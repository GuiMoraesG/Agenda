using Microsoft.EntityFrameworkCore;
using Agenda.Models;

namespace Agenda.Data
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Contact> Contact { get; set; }
    }
}
