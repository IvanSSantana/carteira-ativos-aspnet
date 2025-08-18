using CarteiraAtivos.Data;
using CarteiraAtivos.Dtos;
using CarteiraAtivos.Helpers;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories
{
   public class UsuarioRepository : IUsuarioRepository
   {
      private readonly DatabaseContext _DbContext;

      public UsuarioRepository(DatabaseContext context)
      {
         _DbContext = context;
      }

      public LoginUsuarioModel BuscarPorEmail(string email)
      {
         return _DbContext.LoginUsuarios
            .FirstOrDefault(u => u.Email == email)!;
      }

      public LoginUsuarioModel BuscarPorLogin(string login)
      {
         return _DbContext.LoginUsuarios
            .FirstOrDefault(u => u.Login == login)!;
      }

      public LoginUsuarioModel CadastrarUsuario(LoginUsuarioModel usuario)
      {
         LoginUsuarioModel? usuarioDB = _DbContext.LoginUsuarios.FirstOrDefault(u => u.Email == usuario.Email);

         if (usuarioDB != null)
         {
            throw new InvalidOperationException("Esse e-mail já está cadastrado no sistema.");
         }

         _DbContext.LoginUsuarios.Add(usuario);
         _DbContext.SaveChanges();

         return usuario;
      }

      public bool RedefinirSenhaUsuario(LoginUsuarioModel usuario)
      {
         LoginUsuarioModel? usuarioDB = _DbContext.LoginUsuarios.FirstOrDefault(u => u.Email == usuario.Email);

         if (usuarioDB == null)
         {
            throw new InvalidOperationException("Conta não encontrada.");
         }

         usuarioDB.Senha = usuario.Senha!;

         _DbContext.Update(usuarioDB);
         _DbContext.SaveChanges();
         return true;
      }
   }
}