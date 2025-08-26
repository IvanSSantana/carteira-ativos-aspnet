using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Services;
using CarteiraAtivos.Dtos;

namespace CarteiraAtivos.ViewComponents
{
    public class GraficoHistoricoViewComponent : ViewComponent
    {
        private readonly IApiFinanceiraService _serviceApi;

        public GraficoHistoricoViewComponent(IApiFinanceiraService serviceApi)
        {
            _serviceApi = serviceApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(string ticker)
        {
            // Busca os preços históricos da API (valores diários dos últimos 30 dias)
            // OBS: era o que o plano gratuito da API permitia.
            List<PrecoHistoricoDto> historico = await _serviceApi.ObterHistoricoDePrecos(ticker, "1d", "1mo");

            return View(historico);
        }
    }
}
