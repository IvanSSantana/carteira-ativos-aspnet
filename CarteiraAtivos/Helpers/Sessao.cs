using System.Text.Json;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Helpers
{
   public class Sessao : ISessao
   {
      private readonly IHttpContextAccessor _httpContext;

      public Sessao(IHttpContextAccessor httpContext)
      {
         _httpContext = httpContext;
      }

      public void CriarSessaoLogin(LoginUsuarioModel usuario)
      {
         string json = JsonSerializer.Serialize(usuario);
         _httpContext.HttpContext!.Session.SetString("usuarioLogado", json);
      }

      public void RemoverSessaoLogin()
      {
         _httpContext.HttpContext!.Session.Remove("usuarioLogado");
      }

      public LoginUsuarioModel? VerificarSessaoLogin()
      {
         string sessaoUsuario = _httpContext.HttpContext!.Session.GetString("usuarioLogado");

         if (string.IsNullOrEmpty(sessaoUsuario)) { return null; }

         return JsonSerializer.Deserialize<LoginUsuarioModel>(sessaoUsuario);
      }
   }
}