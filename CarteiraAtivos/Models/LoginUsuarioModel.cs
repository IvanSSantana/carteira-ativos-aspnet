using System.ComponentModel.DataAnnotations;


// Model responsável por armazenar os dados de login do usuário.
namespace CarteiraAtivos.Models
{
    public class LoginUsuarioModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo deve conter um endereço de e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.", MinimumLength = 6)]
        public string Senha { get; set; }
    }
}