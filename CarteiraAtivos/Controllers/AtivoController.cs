using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Models;
using CarteiraAtivos.Repositories;
using CarteiraAtivos.Helpers;
using CarteiraAtivos.Filters;
using AutoMapper;
using CarteiraAtivos.Dtos;

namespace CarteiraAtivos.Controllers;

[PaginaUsuarioLogado]
public class AtivoController : Controller
{   // Injeções de dependência
   private readonly IUsuarioRepositorio _usuarioRepositorio;
   private readonly IAtivoRepositorio _ativoRepositorio;
   private readonly ISessao _sessao;
   private readonly IMapper _mapper;

   public AtivoController(IUsuarioRepositorio usuarioRepositorio,
               IAtivoRepositorio ativoRepositorio, ISessao sessao, IMapper mapper)
   {
      _usuarioRepositorio = usuarioRepositorio;
      _ativoRepositorio = ativoRepositorio;
      _sessao = sessao;
      _mapper = mapper;
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
   public async Task<IActionResult> Criar(AtivoCreateDto ativoDto)
   {
      if (ModelState.IsValid && ModelState != null)
      {
         try
         {
            LoginUsuarioModel usuarioLogado = _sessao.VerificarSessaoLogin()!;

            if (usuarioLogado == null) { return RedirectToAction("Index", "LoginUsuario"); }

            AtivoModel ativoDB = _ativoRepositorio.BuscarPorTickerEUsuarioId(ativoDto.Ticker, usuarioLogado.Id);

            if (ativoDB != null)
            {
               TempData["Erro"] = "Esse ativo já está registrado.";
               return View(ativoDto);
            }

            AtivoModel ativoEnviado = await _ativoRepositorio.CadastrarAtivo(ativoDto);

            if (ativoEnviado == null) { return View(ativoDto); }

            TempData["Sucesso"] = "Ativo cadastrado com sucesso!";
            return View();
         }
         catch (System.Exception erro)
         {
            TempData["Erro"] = $"Houve um erro durante o cadastro. Detalhe do erro: {erro}";
            return View(ativoDto);
         }
      }

      TempData["Erro"] = "Dados inválidos.";
      return View(ativoDto);
   }

   [HttpGet]
   public IActionResult Editar(int ativoId)
   {
      int usuarioId = _sessao.VerificarSessaoLogin()!.Id;
      AtivoModel ativoDb = _ativoRepositorio.BuscarPorIdEUsuarioId(ativoId, usuarioId);
      AtivoCreateDto ativoDtoModel = _mapper.Map<AtivoCreateDto>(ativoDb);

      return View(ativoDtoModel);
   }

   [HttpPost]
   public async Task<IActionResult> Editar(AtivoCreateDto ativoDto)
   {
      try
      {
         int usuarioId = _sessao.VerificarSessaoLogin()!.Id;
         var ativoDb = _ativoRepositorio.BuscarPorIdEUsuarioId(ativoDto.Id, usuarioId);

         if (!ModelState.IsValid)
         {
            TempData["Erro"] = "Erro ao buscar ativo no banco. Por favor, tente novamente.";
            return View("Index", ativoDto);
         }
         
         if (ativoDb == null || ativoDb.LoginUsuarioId != usuarioId)
         {
            TempData["Erro"] = "Edição inválida ou acesso negado.";
            return View("Index", ativoDto);
         }

         await _ativoRepositorio.EditarAtivo(ativoDto);
         TempData["Sucesso"] = "Ativo editado com sucesso!";
         return RedirectToAction("Index");
      }
      catch (System.Exception erro)
      {  
         TempData["Erro"] = $"Houve um erro durante a edição. Detalhes: {erro}";
         return View(ativoDto);
      }
   }
}
