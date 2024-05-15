using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class Login
    {
        [Required(ErrorMessage = "O campo {0}, precisa ser preenchido")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0}, precisa ser preenchido")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O campo {0}, precida ter entre {2} e {1} caracteres")]
        public string Password { get; set; }

        public Login() { }
        public Login(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
