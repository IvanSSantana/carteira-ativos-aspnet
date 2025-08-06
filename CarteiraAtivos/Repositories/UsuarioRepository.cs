using CarteiraAtivos.Data;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories
{
   public class UsuarioRepositorio : IUsuarioRepositorio
   {
      private readonly DatabaseContext _DbContext;

      public UsuarioRepositorio(DatabaseContext context)
      {
         _DbContext = context;
      }

      public LoginUsuarioModel BuscarPorLogin(string login)
      {
         return _DbContext.LoginUsuarios
            .FirstOrDefault(u => u.Login == login);
      }

      public LoginUsuarioModel CadastrarUsuario(LoginUsuarioModel usuario)
      {
         LoginUsuarioModel usuarioDB = _DbContext.LoginUsuarios.FirstOrDefault(u => u.Email == usuario.Email && u.Login == usuario.Login);

         if (usuarioDB != null)
         {
            throw new InvalidOperationException("Esse usuário já está cadastrado no sistema.");
         }

         _DbContext.LoginUsuarios.Add(usuario);
         _DbContext.SaveChanges();

         return usuario;
      }

      public void RedefinirSenhaUsuario()
      {
         // Implementação da redefinição de senha
      }
   }
}