using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Models;
using CarteiraAtivos.Repositories;
using CarteiraAtivos.Helpers;
using CarteiraAtivos.Filters;

namespace CarteiraAtivos.Controllers;

[PaginaUsuarioLogado]
public class AtivoController : Controller
{   // Injeções de dependência
   private readonly IUsuarioRepositorio _usuarioRepositorio;
   private readonly IAtivoRepositorio _ativoRepositorio;
   private readonly ISessao _sessao;

   public AtivoController(IUsuarioRepositorio usuarioRepositorio,
               IAtivoRepositorio ativoRepositorio, ISessao sessao)
   {
      _usuarioRepositorio = usuarioRepositorio;
      _ativoRepositorio = ativoRepositorio;
      _sessao = sessao;
   }

   public IActionResult Index()
   {
      LoginUsuarioModel usuarioLogado = _sessao.VerificarSessaoLogin()!;
      List<AtivoModel> ativos = _ativoRepositorio.BuscarTodosAtivos(usuarioLogado.Id);
      return View(ativos);
   }

   [HttpGet]
   public IActionResult Criar()
   {
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Criar(AtivoModel ativoModel)
   {
      if (ModelState.IsValid && ModelState != null)
      {
         try
         {  
            // Insere dinâmicamente o Id no Model pelo Login
            LoginUsuarioModel UsuarioLogado = _sessao.VerificarSessaoLogin();

            if (UsuarioLogado == null) { return RedirectToAction("Index", "LoginUsuario"); }

            ativoModel.LoginUsuarioId = UsuarioLogado.Id;

            AtivoModel ativoEnviado = await _ativoRepositorio.CadastrarAtivo(ativoModel);

            if (ativoEnviado == null) { return View(ativoModel); }

            TempData["Sucesso"] = "Ativo cadastrado com sucesso!";
            return View();
         }
         catch (System.Exception erro)
         {
            TempData["Erro"] = $"Houve um erro durante o cadastro. Detalhe do erro: {erro}";
            return View(ativoModel);
         }
      }

      TempData["Erro"] = "Dados inválidos.";
      return View(ativoModel);
   }
}
