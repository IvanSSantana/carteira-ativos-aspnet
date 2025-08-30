using System.ComponentModel.DataAnnotations;
using CarteiraAtivos.Helpers;

namespace CarteiraAtivos.Dtos;

public class LoginUsuarioIndexDto // Utilizado para o login (não cadastro)
{
   [Required(ErrorMessage = "O login é obrigatório.")]
   [StringLength(50, ErrorMessage = "O login deve ter entre 3 e 50 caracteres.", MinimumLength = 3)]
   public required string Login { get; set; }

   [Required(ErrorMessage = "A senha é obrigatória.")]
   [StringLength(100, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.", MinimumLength = 6)]
   public required string Senha { get; set; }

   public void SenhaHash()
   {
      Senha = Senha.GerarHash();
   }
}

public class LoginUsuarioCreateDto // Utilizada para cadastro
{
   [Key]
   public int Id { get; set; }

   [Required(ErrorMessage = "O nome é obrigatório.")]
   [StringLength(100, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.", MinimumLength = 3)]
   public required string Nome { get; set; }

   [Required(ErrorMessage = "O login é obrigatório.")]
   [StringLength(50, ErrorMessage = "O login deve ter entre 3 e 50 caracteres.", MinimumLength = 3)]
   public required string Login { get; set; }

   [Required(ErrorMessage = "O e-mail é obrigatório.")]
   [EmailAddress(ErrorMessage = "O campo deve conter um endereço de e-mail válido.")]
   public required string Email { get; set; }

   [Required(ErrorMessage = "A senha é obrigatória.")]
   [StringLength(100, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.", MinimumLength = 6)]
   public required string Senha { get; set; }

   public void SenhaHash()
   {
      Senha = Senha.GerarHash();
   } 
}

public class LoginUsuarioUpdateDto // Utilizado redefinição de senha
{
    public required string Token { get; set; }
    
   [Required(ErrorMessage = "A senha é obrigatória.")]
   [StringLength(100, ErrorMessage = "A senha deve ter, no mínimo, 6 caracteres.", MinimumLength = 6)]
   [DataType(DataType.Password)]
   public string? Senha { get; set; }

   [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
   [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
   [DataType(DataType.Password)]
   public string? ConfirmarSenha { get; set; }

   public void SenhaHash()
   {
      Senha = Senha!.GerarHash();
   }
}

public class LoginUsuarioEmailDto // Utilizado redefinição de senha
{
   [Required(ErrorMessage = "O e-mail é obrigatório.")]
   [EmailAddress(ErrorMessage = "O campo deve conter um endereço de e-mail válido.")]
   public required string Email { get; set; }
}