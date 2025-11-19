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

    public async Task<IActionResult> Preguntar(PerfilesPersonalizados perfilPersonalizado, Productos producto)
    {
        var gemini = new GeminiModel();
        var respuesta = await gemini.PreguntarAsync(perfilPersonalizado, producto);

        ViewBag.Respuesta = respuesta;
        return View();
    }
}