using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Services; // supondo que você tenha um service
using CarteiraAtivos.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            // Busca os preços históricos da API (valores diários do último mês)
            // OBS: era o que o plano gratuito da API permitia.
            List<PrecoHistoricoDto> historico = await _serviceApi.ObterHistoricoDePrecos(ticker, "1d", "1mo");

            return View(historico);
        }
    }
}
