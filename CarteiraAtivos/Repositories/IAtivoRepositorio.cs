using CarteiraAtivos.Dtos;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories
{
    public interface IAtivoRepositorio
    {
        Task<AtivoModel> CadastrarAtivo(AtivoCreateDto ativo);
        List<AtivoModel> BuscarTodosAtivos(int usuarioId);
    }
}