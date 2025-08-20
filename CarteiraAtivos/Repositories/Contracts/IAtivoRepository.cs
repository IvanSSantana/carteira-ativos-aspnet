using CarteiraAtivos.Dtos;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories
{
    public interface IAtivoRepository
    {
        // CRUD
        Task<AtivoModel> CadastrarAtivo(AtivoModel ativo);
        Task<AtivoModel> EditarAtivo(AtivoModel ativo);
        bool DeletarAtivo(int ativoId);

        // Queries somente de busca
        List<AtivoModel> BuscarTodosAtivos(int usuarioId);
        AtivoModel BuscarPorIdEUsuarioId(int ativoId, int usuarioId);
        AtivoModel BuscarPorTickerEUsuarioId(string ticker, int usuarioId);
    }
}