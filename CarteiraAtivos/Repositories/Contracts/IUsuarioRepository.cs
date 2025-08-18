using CarteiraAtivos.Dtos;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories
{
    public interface IUsuarioRepository
    {
        LoginUsuarioModel CadastrarUsuario(LoginUsuarioModel usuario);
        bool RedefinirSenhaUsuario(LoginUsuarioModel usuario);
        LoginUsuarioModel BuscarPorLogin(string login);
        LoginUsuarioModel BuscarPorEmail(string email); // Ainda preciso refatorar o restante da lógica do sisterma
    }
}