using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nutrinfo.Models;

namespace Nutrinfo.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public UsuarioController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Registrarse(string nombre, string apellido, string contrasena, string email)
    {
        string vista;
        int registroValido;
        registroValido = Database.RegistrarUsuario(nombre, apellido, contrasena, email);

        if (registroValido == 0)
        {
            vista = "PerfilPersonalizado";
        }
        else
        {
            ViewBag.error = "Error";
            vista = "Registro";
        }

        return View(vista);
    }
}
