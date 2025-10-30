using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using Nutrinfo.Models;

namespace Nutrinfo.Controllers;

public class ProductoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public ProductoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    private string NombreUsuario()
    {
        string userJson = HttpContext.Session.GetString("Usuario");
        Usuarios user = Objeto.StringToObject<Usuarios>(userJson);
        if (user != null)
            return user.Nombre + " " + user.Apellido;
        else
            return "";
    }

    public IActionResult Buscador()
    {
        ViewBag.ListarProductos = Database.ListarProductos();
        return View();
    }

    [HttpPost]
    public IActionResult Buscador(string busqueda)
    {
        ViewBag.ListarProductos = Database.BuscarProductos(busqueda);
        return View();
    }

    public IActionResult InformacionProducto() {
        return View();
    }
}