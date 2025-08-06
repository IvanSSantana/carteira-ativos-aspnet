using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Models;
using CarteiraAtivos.Repositories;
using CarteiraAtivos.Helpers;

namespace CarteiraAtivos.Controllers;

public class LoginUsuarioController : Controller
{   // Injeções de dependência
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public LoginUsuarioController(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    public IActionResult Index()
    {
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
    public IActionResult Cadastrar(LoginUsuarioModel usuario)
    {
        if (ModelState.IsValid)
        {
            LoginUsuarioModel usuarioDB = _usuarioRepositorio.BuscarPorLogin(usuario.Login);

            if (usuarioDB != null)
            {
                TempData["Erro"] = "Já existe um usuário cadastrado com este login.";
                return View(usuario);
            }

            try
            {
                usuario.SenhaHash(); // Criptografa a senha
                _usuarioRepositorio.CadastrarUsuario(usuario);
                TempData["Sucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Index", "Home");   
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao cadastrar usuário: " + ex.Message;
                return View(usuario);
            }
        }
        else
        {
            TempData["Erro"] = "Dados inválidos ou incompletos. Por favor, tente novamente.";
            return View(usuario);
        }
    }
    
    [HttpPost]
    public IActionResult Login(LoginUsuarioModel usuario)
    {
        var usuarioDB = _usuarioRepositorio.BuscarPorLogin(usuario.Login);
        if (ModelState != null)
        {
            if (usuarioDB == null || usuarioDB.Senha != usuario.Senha.GerarHash())
            {
                TempData["Erro"] = "Usuário ou senha inválidos.";
                return View("Index", usuario);
            }

            return RedirectToAction("Index", "Home");
        }

        TempData["Erro"] = "Dados inválidos ou incompletos.";
        return View("Index", usuario);
    }
}