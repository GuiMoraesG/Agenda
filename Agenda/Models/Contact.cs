namespace Agenda.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public User User { get; set; }

        public Contact() { }

        public Contact(int id, string name, string email, string phone, User user)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            User = user;
        }
    }
}
