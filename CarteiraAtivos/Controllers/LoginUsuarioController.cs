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
    private readonly IUsuarioRepository _usuarioRepositorio;
    private readonly ISessao _sessao;
    private readonly IMapper _mapper;
    private readonly IEmail _email;
    private readonly IRedefinicaoSenhaRepository _redefinicaoSenhaRepo;

    public LoginUsuarioController(IUsuarioRepository usuarioRepositorio,
                ISessao sessao, IMapper mapper, IEmail email, IRedefinicaoSenhaRepository redefinicaoSenhaRepo)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _sessao = sessao;
        _mapper = mapper;
        _email = email;
        _redefinicaoSenhaRepo = redefinicaoSenhaRepo;
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

    public IActionResult EmailRedefinirSenha()
    {
        return View();
    }

    [HttpPost]
    public IActionResult EnviarCodigoRedefinirSenha(LoginUsuarioEmailDto usuario)
    {   
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        LoginUsuarioModel usuarioDB = _usuarioRepositorio.BuscarPorEmail(usuario.Email);
        if (usuarioDB == null)
        {
            TempData["Erro"] = "E-mail não cadastrado no sistema.";
            return RedirectToAction("EmailRedefinirSenha");
        }

        var codigo = Guid.NewGuid().ToString().Substring(0, 8);
        _redefinicaoSenhaRepo.Criar(usuario.Email, codigo);

        // Gera o link para a view RedefinirSenha
        string link = Url.Action("RedefinirSenha", "LoginUsuario", new { codigo }, Request.Scheme)!;
        _email.Enviar(usuario.Email, "Link para redefinição de senha", $"Clique aqui para redefinir sua senha: {link}");

        TempData["Sucesso"] = "Enviamos instruções para redefinir a senha no seu e-mail.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult RedefinirSenha(string Codigo)
    {
        if (Codigo == null || _redefinicaoSenhaRepo.BuscarPorCodigo(Codigo) == null) return RedirectToAction("Index");

        LoginUsuarioUpdateDto usuarioDto = new()
        {
            Codigo = Codigo
        };

        return View(usuarioDto);
    }

    [HttpPost]
    public IActionResult RedefinirSenha(LoginUsuarioUpdateDto usuarioDto)
    {
        if (!ModelState.IsValid)
        {
            return View(usuarioDto);
        }

        RedefinicaoSenhaModel redefinirDB = _redefinicaoSenhaRepo.BuscarPorCodigo(usuarioDto.Codigo);
        LoginUsuarioModel usuarioDB = _usuarioRepositorio.BuscarPorEmail(redefinirDB.Email);

        if (usuarioDto.Codigo != redefinirDB.Codigo || redefinirDB == null || usuarioDB == null)
        {
            TempData["Erro"] = "Código inválido, expirado ou usuário não encontrado.";
            return View(usuarioDto);
        }

        try
        {
            usuarioDto.SenhaHash();
            usuarioDB.Senha = usuarioDto.Senha!;

            _redefinicaoSenhaRepo.MarcarComoUtilizado(redefinirDB);
            _usuarioRepositorio.RedefinirSenhaUsuario(usuarioDB);
            TempData["Sucesso"] = "Senha atualizada com sucesso!";
            return RedirectToAction("Index", "LoginUsuario");
        }
        catch (System.Exception erro)
        {
            TempData["Erro"] = $"Houve um erro durante a redefinição de senha. Detalhes: {erro}";
            return View(usuarioDto);
        }
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