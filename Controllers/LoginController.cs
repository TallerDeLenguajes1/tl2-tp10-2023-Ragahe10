using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }
    public IActionResult Index()
    {
        return View(new UsuarioLogin());
    }

    public IActionResult Login(UsuarioLogin userLog)
    {   if(ModelState.IsValid){
            var user = _usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.NombreDeUsuario == userLog.Nombre && u.Pass == userLog.Pass);
            if(user == null) return RedirectToAction("Index");
            LogearUsuario(user);

            return RedirectToRoute(new{controller = "Home", action = "Index"});
        }
        return RedirectToAction("Index");
    }
    private void LogearUsuario(Usuario user){
        
        HttpContext.Session.SetString("User",user.NombreDeUsuario);
        HttpContext.Session.SetString("Rol",user.Rol);
        HttpContext.Session.SetInt32("id",user.Id);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}