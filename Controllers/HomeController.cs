using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using Nutrinfo.Models;

namespace Nutrinfo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        ViewBag.NombreUsuario = NombreUsuario();
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

    
}
