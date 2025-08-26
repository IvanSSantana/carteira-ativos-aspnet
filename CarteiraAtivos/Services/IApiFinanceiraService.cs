using CarteiraAtivos.Dtos;

namespace CarteiraAtivos.Services
{
    public interface IApiFinanceiraService
    {
        Task<AtivoApiDto> ObterDadosDoAtivo(AtivoCreateDto ativoModel);
        Task<List<PrecoHistoricoDto>> ObterHistoricoDePrecos(string ticker, string intervalo, string range);
    }
}