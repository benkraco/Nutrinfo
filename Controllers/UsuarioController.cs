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

    public IActionResult Registrarse()
    {
        ViewBag.error = "";
        return View();
    }

    [HttpPost]
    public IActionResult Registrarse(string nombre, string apellido, string contrasena, string email)
    {
        Usuarios usuario = new Usuarios();
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
            vista = "Registrarse";
        }

        HttpContext.Session.SetString("Usuario", "nombre" : nombre, "apellido" : apellido, "contrasena" : contrasena, "email" : email);

        return View(vista);
    }

    public IActionResult IniciarSesion()
    {
        ViewBag.error = "";
        return View();
    }

    [HttpPost]
    public IActionResult IniciarSesion(string email, string contrasena)
    {
        string vista;
        int inicioValido;
        inicioValido = Database.IniciarSesion(email, contrasena);

        if (inicioValido == 1)
        {
            vista = "Home";
        }
        else
        {
            ViewBag.error = "Error";
            vista = "IniciarSesion";
        }

        return View(vista);
    }

    public IActionResult PerfilPersonalizado()
    {
        ViewBag.error = "";
        return View();
    }

    [HttpPost]
    public IActionResult PerfilPersonalizado(int idUsuario, string alergias, string intolerancias, string enfermedades, string cultura, string estiloDeVida, string dieta)
    {
        Database.CrearPerfilPersonalizado(idUsuario, alergias, intolerancias, enfermedades, cultura, estiloDeVida, dieta);
        return View("Bienvenida");
    }
}
