using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            ViewUsuarioListado usuarios = new ViewUsuarioListado(_usuarioRepository.GetAllUsuarios());
            return View(usuarios);
        }else{
            ViewUsuarioListado usuarios = new ViewUsuarioListado(_usuarioRepository.GetAllUsuarios().FindAll(u => u.Id ==  HttpContext.Session.GetInt32("id")));
            return View(usuarios);
        }
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        if(isAdmin()){
            return View(new ViewUsuarioAdd());
        }
        return RedirectToRoute(new{controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult CrearUsuario(ViewUsuarioAdd viewUsuarioAdd)
    {
        if(ModelState.IsValid){
            if(isAdmin()){
                var usuario = new Usuario(viewUsuarioAdd);
                _usuarioRepository.AddUsuario(usuario);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }
        return RedirectToAction("CrearUsuario");
    }
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {   
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            var viewUsuarioUpdate = new ViewUsuarioUpdate(_usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.Id == id));
            return View(viewUsuarioUpdate);
        }else{
            if(HttpContext.Session.GetInt32("id")==id){
                var viewUsuarioUpdate = new ViewUsuarioUpdate(_usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.Id == id));
                return View("ModificarUsuarioOperador",viewUsuarioUpdate);
                //return RedirectToAction("ModificarUsuario",usuario);
            }else{
                return RedirectToAction("Index");
            }
        }
    }
    [HttpPost]
    public IActionResult ModificarUsuario(int id, ViewUsuarioUpdate viewUsuarioUpdate)
    {
        if(ModelState.IsValid){
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                var usuario = new Usuario(viewUsuarioUpdate);
                _usuarioRepository.UpdateUsuario(id,usuario);
            }else{
                if(HttpContext.Session.GetInt32("id")==id){
                    var usuario = new Usuario(viewUsuarioUpdate);
                    _usuarioRepository.UpdateUsuario(id,usuario);
                }
            }
            return RedirectToAction("Index");
        }
        return RedirectToAction("ModificarUsuario");
    }
    public IActionResult EliminarUsuario(int idUsuario)
    {
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            _usuarioRepository.DeleteUsuario(idUsuario);
        }else{
            if(HttpContext.Session.GetInt32("id")==idUsuario){
                _usuarioRepository.DeleteUsuario(idUsuario);
            }
        }
        return RedirectToAction("Index");
    }
    private bool isAdmin(){
        if(HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador"){
            return true;
        }
        return false;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
