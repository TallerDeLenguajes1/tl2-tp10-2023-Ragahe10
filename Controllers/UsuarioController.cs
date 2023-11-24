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
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            ViewUsuarioListado usuarios = new ViewUsuarioListado(usuarioRepository.GetAllUsuarios());
            return View(usuarios);
        }else{
            ViewUsuarioListado usuarios = new ViewUsuarioListado(usuarioRepository.GetAllUsuarios().FindAll(u => u.Id ==  HttpContext.Session.GetInt32("id")));
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
        if(isAdmin()){
            var usuario = new Usuario(viewUsuarioAdd);
            usuarioRepository.AddUsuario(usuario);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new{controller = "Login", action = "Index"});
    }
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {   
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            var viewUsuarioUpdate = new ViewUsuarioUpdate(usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.Id == id));
            return View(viewUsuarioUpdate);
        }else{
            if(HttpContext.Session.GetInt32("id")==id){
                var viewUsuarioUpdate = new ViewUsuarioUpdate(usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.Id == id));
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
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            var usuario = new Usuario(viewUsuarioUpdate);
            usuarioRepository.UpdateUsuario(id,usuario);
        }else{
            if(HttpContext.Session.GetInt32("id")==id){
                var usuario = new Usuario(viewUsuarioUpdate);
                usuarioRepository.UpdateUsuario(id,usuario);
            }
        }
        return RedirectToAction("Index");
    }
    public IActionResult EliminarUsuario(int idUsuario)
    {
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            usuarioRepository.DeleteUsuario(idUsuario);
        }else{
            if(HttpContext.Session.GetInt32("id")==idUsuario){
                usuarioRepository.DeleteUsuario(idUsuario);
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
