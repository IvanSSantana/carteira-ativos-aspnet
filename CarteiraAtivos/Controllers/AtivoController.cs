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
            LoginUsuarioModel UsuarioLogado = _sessao.VerificarSessaoLogin();

            if (UsuarioLogado == null) { return RedirectToAction("Index", "LoginUsuario"); }

            // Insere dinâmicamente o Id no Model pelo Login
            ativoDto.LoginUsuarioId = UsuarioLogado.Id;

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
}
