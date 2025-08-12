using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Models;
using CarteiraAtivos.Repositories;
using CarteiraAtivos.Helpers;
using AutoMapper;
using CarteiraAtivos.Dtos;

namespace CarteiraAtivos.Controllers;

public class LoginUsuarioController : Controller
{   // Injeções de dependência
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISessao _sessao;
    private readonly IMapper _mapper;

    public LoginUsuarioController(IUsuarioRepositorio usuarioRepositorio,
                ISessao sessao, IMapper mapper)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _sessao = sessao;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        // Verifica se o usuário já não está logado
        if (_sessao.VerificarSessaoLogin() != null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult RedefinirSenha()
    {
        // Lógica para redefinir a senha
        return View();
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(LoginUsuarioCreateDto usuarioDto)
    {
        if (ModelState.IsValid)
        {
            LoginUsuarioModel usuarioModel = _mapper.Map<LoginUsuarioModel>(usuarioDto);
            LoginUsuarioModel usuarioDB = _usuarioRepositorio.BuscarPorLogin(usuarioModel.Login);

            if (usuarioDB != null)
            {
                TempData["Erro"] = "Já existe um usuário cadastrado com este login.";
                return View(usuarioDto);
            }

            try
            {
                usuarioModel.SenhaHash(); // Criptografa a senha
                _usuarioRepositorio.CadastrarUsuario(usuarioModel);
                TempData["Sucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao cadastrar usuário: " + ex.Message;
                return View(usuarioDto);
            }
        }
        else
        {
            TempData["Erro"] = "Dados inválidos ou incompletos. Por favor, tente novamente.";
            return View(usuarioDto);
        }
    }
    
    [HttpPost]
    public IActionResult Login(LoginUsuarioIndexDto usuarioDto)
    {
        var usuarioDB = _usuarioRepositorio.BuscarPorLogin(usuarioDto.Login);
        if (ModelState != null)
        {
            if (usuarioDB == null || usuarioDB.Senha != usuarioDto.Senha.GerarHash())
            {
                TempData["Erro"] = "Usuário ou senha inválidos.";
                return View("Index", usuarioDto);
            }
            
            _sessao.CriarSessaoLogin(usuarioDB);
            return RedirectToAction("Index", "Home");
        }

        TempData["Erro"] = "Dados inválidos ou incompletos.";
        return View("Index", usuarioDto);
    }

    public IActionResult SairLogin()
    {
        try
        {
            _sessao.RemoverSessaoLogin();
            return RedirectToAction("Index", "LoginUsuario");
        }
        catch (Exception ex)
        {
            TempData["Erro"] = "Erro ao sair: " + ex.Message;
            return RedirectToAction("Index", "Home");
        }
    }
}