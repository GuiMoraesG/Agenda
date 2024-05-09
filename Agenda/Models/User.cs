namespace Agenda.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Cdate { get; set; }

        public ICollection<Contact> Contacts = new List<Contact>();

        public User() { }
        public User(int id, string name, string email, string password, DateTime cdate)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Cdate = cdate;
        }
    }
}
