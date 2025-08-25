using Microsoft.AspNetCore.Mvc;

namespace CarteiraAtivos.ViewComponents
{
    public class BarraPesquisaAtivoViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(bool redirecionamento)
        {
            return Task.FromResult<IViewComponentResult>(View(redirecionamento));
        }
    }
}
    