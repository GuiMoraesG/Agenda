using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(60)]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Celular")]
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
