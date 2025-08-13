using CarteiraAtivos.Dtos;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories
{
    public interface IAtivoRepositorio
    {
        Task<AtivoModel> CadastrarAtivo(AtivoCreateDto ativo);
        Task<AtivoModel> EditarAtivo(AtivoCreateDto ativo);
        List<AtivoModel> BuscarTodosAtivos(int usuarioId);
        AtivoModel BuscarPorIdEUsuarioId(int ativoId, int usuarioId);
        AtivoModel BuscarPorTickerEUsuarioId(string ticker, int usuarioId);
    }
}