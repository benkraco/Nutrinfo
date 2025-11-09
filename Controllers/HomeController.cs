using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        var data = HttpContext.Session.GetString("Usuario");
        if (data != null)
        {
            var usuario = JsonConvert.DeserializeObject<Usuarios>(data);
            ViewBag.UsuarioLogeado = usuario;
        }
        else
        {
            ViewBag.UsuarioLogeado = null;
        }
        return View();
    }
}
