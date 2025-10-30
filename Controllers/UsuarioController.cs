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

    private string NombreUsuario()
    {
        string userJson = HttpContext.Session.GetString("Usuario");
        Usuarios user = Objeto.StringToObject<Usuarios>(userJson);
        if (user!=null)
            return user.Nombre + " " + user.Apellido;
        else 
            return "";
    }

    [HttpPost]
    public IActionResult Registrarse(string nombre, string apellido, string contrasena, string email)
    {
        string vista;
        int registroValido;
        registroValido = Database.RegistrarUsuario(nombre, apellido, contrasena, email);

        if (registroValido == 0)
        {
            HttpContext.Session.SetString("Usuario", Objeto.ObjectToString(Database.traerUsuarioRegistro(nombre, apellido, contrasena)));
            vista = "PerfilPersonalizado";
        }
        else
        {
            ViewBag.error = "Error";
            vista = "Registrarse";
        }

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
            HttpContext.Session.SetString("Usuario", Objeto.ObjectToString(Database.traerUsuarioLogin(email, contrasena)));
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
