using CarteiraAtivos.Models;

namespace CarteiraAtivos.Helpers
{
    public interface ISessao
    {
        void CriarSessaoLogin(LoginUsuarioModel usuario);
        void RemoverSessaoLogin();
        LoginUsuarioModel? VerificarSessaoLogin();
    }
}