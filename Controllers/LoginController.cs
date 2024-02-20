using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Repository;
using Proyecto.ViewModels;

namespace Proyecto.Controllers;

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
        return View(new ViewUsuarioLogin());
    }

    public IActionResult LogIn(ViewUsuarioLogin user){
        try {
            if(ModelState.IsValid){
                Usuario usuario = _usuarioRepository.GetUsuarioLogin(user);
                if(usuario!=null){
                    LogInUser(usuario);
                    _logger.LogInformation("INICIO DE SESION -Usuario: "+usuario.Nombre_de_usuario);
                    return RedirectToRoute(new{controller = "Home" , action = "Index"});
                }else{
                    ModelState.AddModelError(nameof(ViewUsuarioLogin.Name), "El usuario y/o contraseña son incorrectos");
                }
                _logger.LogWarning("ACCESO INVALIDO -Usuario: "+user.Name+" -Contraseña: "+user.Pass);
            }
            return View("Index",user);
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    public IActionResult LogOut(){
        try{
            LogOutUser();
            return RedirectToRoute(new{controller = "Home" , action = "Index"});
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    private void LogInUser (Usuario usuario){
        HttpContext.Session.SetInt32("Id", usuario.Id);
        HttpContext.Session.SetString("User", usuario.Nombre_de_usuario);
        HttpContext.Session.SetString("Rol", usuario.Rol);
        HttpContext.Session.SetString("Imagen", usuario.Imagen);
    }
    private void LogOutUser(){
        HttpContext.Session.Clear();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
