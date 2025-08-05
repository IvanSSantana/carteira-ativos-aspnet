using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Controllers;

public class LoginUsuarioController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}