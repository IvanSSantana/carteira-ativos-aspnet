using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories;

public interface IRedefinicaoSenhaRepository
{
   void Criar(string email, string token);
   RedefinicaoSenhaModel? BuscarPorToken(string token);
   void MarcarComoUtilizado(RedefinicaoSenhaModel redefinicao);
}