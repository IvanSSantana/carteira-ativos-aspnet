using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Services;
using CarteiraAtivos.Dtos;

namespace CarteiraAtivos.ViewComponents
{
    public class BarraPesquisaAtivoViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult<IViewComponentResult>(View());
        }
    }
}
    