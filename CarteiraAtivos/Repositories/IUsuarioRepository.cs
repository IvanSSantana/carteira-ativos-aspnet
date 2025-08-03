using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories
{
    public interface IUsuarioRepositorio
    {
        LoginUsuarioModel CadastrarUsuario(LoginUsuarioModel usuario);
        void RedefinirSenhaUsuario(); // Ainda não implementado
        LoginUsuarioModel BuscarPorLoginESenha(string login, string senha);
    }
}