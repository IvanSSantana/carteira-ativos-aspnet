using Microsoft.AspNetCore.Mvc;

namespace CarteiraAtivos.ViewComponents
{
    public class CardDetalhamentoViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string texto)
        {
            return Task.FromResult<IViewComponentResult>(View("Default", texto));
            // É necessário especificar Default, se não o ASP.NET entende o texto como o nome de uma View
        }
    }
}
    