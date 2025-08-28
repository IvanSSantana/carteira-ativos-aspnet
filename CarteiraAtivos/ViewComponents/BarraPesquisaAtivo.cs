using Microsoft.AspNetCore.Mvc;

namespace CarteiraAtivos.ViewComponents
{
    public class BarraPesquisaAtivoViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(bool redirecionamento, string? ticker)
        {
            ViewBag.Ticker = ticker; // Funciona como TempData, mas só dura aqui. Ideal para visualizações simples.
            return Task.FromResult<IViewComponentResult>(View(redirecionamento));
        }
    }
}
    