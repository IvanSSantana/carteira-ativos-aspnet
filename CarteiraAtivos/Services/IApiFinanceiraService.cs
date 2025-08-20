using CarteiraAtivos.Dtos;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Services
{
    public interface IApiFinanceiraService
    {
        Task<AtivoModel> ObterDadosDoAtivo(AtivoCreateDto ativoModel);
        Task<List<PrecoHistoricoDto>> ObterHistoricoDePrecos(string ticker, string intervalo, string range);
    }
}