using CarteiraAtivos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace CarteiraAtivos.Filters
{
    public class PaginaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("usuarioLogado")!;

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToActionResult("Index", "LoginUsuario", new RouteValueDictionary
                {
                    { "controller", "LoginUsuario" },
                    { "action", "Index" }
                });
            }
            else
            {
                LoginUsuarioModel usuario = JsonSerializer.Deserialize<LoginUsuarioModel>(sessaoUsuario)!;

                if (usuario == null)
                {
                    context.Result = new RedirectToActionResult("Index", "LoginUsuario", new RouteValueDictionary
                    {
                        { "controller", "LoginUsuario" },
                        { "action", "Index" }
                    });
                }
            }
            
            base.OnActionExecuting(context);
        }
    }
}
