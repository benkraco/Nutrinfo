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

    public IActionResult ExitoIniciarSesion()
    {
        Usuarios usuario = Objeto.StringToObject<Usuarios>(HttpContext.Session.GetString("Usuario"));
        if (usuario == null)
        {
            return RedirectToAction("IniciarSesion", "Usuario");
        }
        return View();
    }

    public IActionResult ExitoRegistrarse()
    {
        Usuarios usuario = Objeto.StringToObject<Usuarios>(HttpContext.Session.GetString("Usuario"));
        if (usuario == null)
        {
            return RedirectToAction("IniciarSesion", "Usuario");
        }
        return View();
    }

    public IActionResult ExitoPerfilPersonalizado()
    {
        Usuarios usuario = Objeto.StringToObject<Usuarios>(HttpContext.Session.GetString("Usuario"));
        if (usuario == null)
        {
            return RedirectToAction("IniciarSesion", "Usuario");
        }
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
            vista = "ExitoRegistrarse";
        }
        else
        {
            ViewBag.error = "ERROR - Este mail ya se ha usado";
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
            vista = "ExitoIniciarSesion";
        }
        else
        {
            ViewBag.error = "ERROR - Email o contraseña incorrectos";
            vista = "IniciarSesion";
        }

        return View(vista);
    }

    public IActionResult PerfilPersonalizado()
    {
        Usuarios usuario = Objeto.StringToObject<Usuarios>(HttpContext.Session.GetString("Usuario"));
        if (usuario == null)
        {
            return RedirectToAction("IniciarSesion", "Usuario");
        }
        if (!Database.BuscarPerfilExiste(usuario.Id))
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public IActionResult PerfilPersonalizado(string alergias, string intolerancias, string enfermedades, string cultura, string estiloDeVida, string dieta)
    {
        Usuarios usuario = Objeto.StringToObject<Usuarios>(HttpContext.Session.GetString("Usuario"));
        Database.CrearPerfilPersonalizado(usuario.Id, alergias, intolerancias, enfermedades, cultura, estiloDeVida, dieta);
        return View("ExitoPerfilPersonalizado");
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return View();
    }

    public IActionResult Configuracion()
    {
        return View();
    }
}