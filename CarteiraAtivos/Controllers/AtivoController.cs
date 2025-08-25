using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Models;
using CarteiraAtivos.Repositories;
using CarteiraAtivos.Helpers;
using CarteiraAtivos.Filters;
using AutoMapper;
using CarteiraAtivos.Dtos;
using CarteiraAtivos.Services;

namespace CarteiraAtivos.Controllers;

[PaginaUsuarioLogado]
public class AtivoController : Controller
{   // Injeções de dependência
   private readonly IUsuarioRepository _usuarioRepositorio;
   private readonly IAtivoRepository _ativoRepositorio;
   private readonly ISessao _sessao;
   private readonly IMapper _mapper;
   private readonly IApiFinanceiraService _serviceApi;

   public AtivoController(IUsuarioRepository usuarioRepositorio,
               IAtivoRepository ativoRepositorio, ISessao sessao, IMapper mapper, IApiFinanceiraService serviceApi)
   {
      _usuarioRepositorio = usuarioRepositorio;
      _ativoRepositorio = ativoRepositorio;
      _sessao = sessao;
      _mapper = mapper;
      _serviceApi = serviceApi;
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

   // Async tasks<X> representam tarefas assíncronas que retornam tipo X
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

            AtivoModel ativoApi = await _serviceApi.ObterDadosDoAtivo(ativoDto);
            ativoApi.LoginUsuarioId = _sessao.VerificarSessaoLogin()!.Id;

            AtivoModel ativoEnviado = await _ativoRepositorio.CadastrarAtivo(ativoApi);

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
         AtivoModel ativoDb = _ativoRepositorio.BuscarPorIdEUsuarioId(ativoDto.Id, usuarioId);

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

         AtivoModel ativoAtualizado = await _serviceApi.ObterDadosDoAtivo(ativoDto);

         await _ativoRepositorio.EditarAtivo(ativoAtualizado);
         TempData["Sucesso"] = "Ativo editado com sucesso!";
         return RedirectToAction("Index");
      }
      catch (System.Exception erro)
      {
         TempData["Erro"] = $"Houve um erro durante a edição. Detalhes: {erro}";
         return View(ativoDto);
      }
   }

   [HttpGet]
   public IActionResult ApagarConfirmacao(int ativoId)
   {
      int usuarioId = _sessao.VerificarSessaoLogin()!.Id;
      AtivoModel ativoModel = _ativoRepositorio.BuscarPorIdEUsuarioId(ativoId, usuarioId);

      if (ativoModel == null)
      {
         TempData["Erro"] = "Falha na busca do ativo.";
         return RedirectToAction("Index");
      }

      // Modal de confirmação da exclusão
      return PartialView("_ApagarConfirmacao", ativoModel);
   }

   [HttpPost]
   public IActionResult Apagar(int ativoId)
   {
      try
      {
         int usuarioId = _sessao.VerificarSessaoLogin()!.Id;
         AtivoModel ativoDB = _ativoRepositorio.BuscarPorIdEUsuarioId(ativoId, usuarioId);

         if (ativoDB == null)
         {
            TempData["Erro"] = "Falha na busca do ativo.";
            return RedirectToAction("Index");
         }

         _ativoRepositorio.DeletarAtivo(ativoId);
         TempData["Sucesso"] = "Ativo deletado com sucesso!";
         return RedirectToAction("Index");
      }
      catch (System.Exception erro)
      {
         TempData["Erro"] = $"Ocorreu um problema durante a deleção do ativo. Detalhes: {erro}";
         return RedirectToAction("Index");
      }
   }

   [HttpGet]
   public async Task<IActionResult> Detalhamento(string ticker)
   {
      AtivoCreateDto ativoDto = new()
      {
         Ticker = ticker,
         Cotas = 0 // Provavelmente buscarei do banco posteriormente para exibir relações do usuário com o ativo
      };

      var ativo = await _serviceApi.ObterDadosDoAtivo(ativoDto);

      if (ativo == null)
      {
         TempData["Erro"] = "Ativo não encontrado.";
         return RedirectToAction("Index");
      }
      
      return View(ativo);
   }

   [HttpGet]
   public async Task<IActionResult> PesquisaAtivo(string ticker)
   {
      AtivoCreateDto ativoDto = new()
      {
         Ticker = ticker,
         Cotas = 0
      };

      var ativo = await _serviceApi.ObterDadosDoAtivo(ativoDto);

      if (ativo == null)
      {
         return Json(null);
      }

      AtivoApiDto ativoApiDto = _mapper.Map<AtivoApiDto>(ativo);

      return Json(ativoApiDto); // Serializa pra json
   }
}
