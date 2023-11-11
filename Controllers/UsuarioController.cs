using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        var usuarios = usuarioRepository.GetAllUsuarios();
        return View(usuarios);
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new Usuario());
    }
    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        usuarioRepository.AddUsuario(usuario);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        var usuario = usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.Id == id);
        return View(usuario);
    }
    [HttpPost]
    public IActionResult ModificarUsuario(int id, Usuario usuario)
    {
        usuarioRepository.UpdateUsuario(id,usuario);
        return RedirectToAction("Index");
    }
    public IActionResult EliminarUsuario(int idUsuario)
    {
        usuarioRepository.DeleteUsuario(idUsuario);
        return RedirectToAction("index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
