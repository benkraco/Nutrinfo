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
        ViewBag.busqueda = "";
        ViewBag.ListarProductos = Database.ListarProductos();
        return View();
    }

    [HttpPost]
    public IActionResult Buscador(string busqueda)
    {
        ViewBag.busqueda = busqueda;
        ViewBag.ListarProductos = Database.BuscarProductos(busqueda);
        return View();
    }

    public IActionResult InformacionProducto(int id) {
        ViewBag.Producto = Database.BuscarProductoConID(id);
        Productos producto = Database.BuscarProductoConID(id);
        ViewBag.Ingredientes = Database.BuscarIngredientesConIDProducto(id);
        return View(producto);
    }

    public async Task<IActionResult> Preguntar(Productos producto)
    {
        Usuarios usuario = Objeto.StringToObject<Usuarios>(HttpContext.Session.GetString("Usuario"));
        PerfilesPersonalizados perfilPersonalizado = Database.BuscarPerfilConID(usuario.Id);
        var gemini = new GeminiModel();
        string respuesta = await gemini.PreguntarAsync(perfilPersonalizado, producto);

        return Json(new { respuesta });
    }
}