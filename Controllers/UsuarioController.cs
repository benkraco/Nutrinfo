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

    public IActionResult Exito() {
        return View();
    }

    public IActionResult Registrarse()
    {
        ViewBag.error = "";
        return View();
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
            vista = "Exito";
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
            vista = "Exito";
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
        Usuarios usuario = Objeto.StringToObject<Usuarios>(HttpContext.Session.GetString("Usuario"));
        if (usuario == null) {
            return RedirectToAction("IniciarSesion", "Usuario");
        }
        return View();
    }

    [HttpPost]
    public IActionResult PerfilPersonalizado(int idUsuario, string alergias, string intolerancias, string enfermedades, string cultura, string estiloDeVida, string dieta)
    {
        Database.CrearPerfilPersonalizado(idUsuario, alergias, intolerancias, enfermedades, cultura, estiloDeVida, dieta);
        return View("Bienvenida");
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return View();
    }
}