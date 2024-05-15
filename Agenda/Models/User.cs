using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo {0}, precisa ser preenchido")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} precisa estar entre {2} e {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo {0}, precisa ser preenchido")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0}, precisa ser preenchido")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Campo {0}, precisa ter entre {2} e {1} digitos")]
        public string Password { get; set; }

        [Display(Name = "Última atualização")]
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

        public bool ValidatePass(string senha)
        {
            return Password == senha;
        }
    }
}
