using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories;

public interface IRedefinicaoSenhaRepository
{
   void Criar(string email, string codigo);
   RedefinicaoSenhaModel? BuscarPorCodigo(string codigo);
   void MarcarComoUtilizado(RedefinicaoSenhaModel redefinicao);
}