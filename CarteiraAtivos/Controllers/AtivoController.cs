using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Models;
using CarteiraAtivos.Repositories;

namespace CarteiraAtivos.Controllers;

public class AtivoController : Controller
{   // Injeções de dependência
   private readonly IUsuarioRepositorio _usuarioRepositorio;
   private readonly IAtivoRepositorio _ativoRepositorio;

   public AtivoController(IUsuarioRepositorio usuarioRepositorio,
               IAtivoRepositorio ativoRepositorio)
   {
      _usuarioRepositorio = usuarioRepositorio;
      _ativoRepositorio = ativoRepositorio;
   }

   public IActionResult Index()
   {
      List<AtivoModel> ativos = _ativoRepositorio.BuscarTodosAtivos();
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
      if (ModelState.IsValid & ModelState != null)
      {
         try
         {
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
