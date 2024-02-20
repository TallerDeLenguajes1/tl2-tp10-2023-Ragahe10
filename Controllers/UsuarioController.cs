using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Repository;
using Proyecto.ViewModels;

namespace Proyecto.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository _usuarioRepository;
    private ITableroRepository _tableroRepository;
    private ITareaRepository _tareaRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index()
    {
        try{
            if(HttpContext.Session.GetString("Rol")==null) return RedirectToRoute(new{controller = "Login", action="Index"});
            ViewUsuarioListado ListaDeUsuarios = new ViewUsuarioListado (_usuarioRepository.GetAllUsuarios());
            return View(ListaDeUsuarios);
        }catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new ViewUsuarioAdd());
    }
    [HttpPost]
    public IActionResult CrearUsuario(ViewUsuarioAdd user)
    {
        try
        {
            if(ModelState.IsValid){
                Usuario usuario = new Usuario(user);
                if (!_usuarioRepository.Existe(usuario.Nombre_de_usuario)){
                    if(user.Pass == user.PassControl){
                        _usuarioRepository.AddUsuario(usuario);
                        ViewUsuarioLogin usuarioLogin = new ViewUsuarioLogin();
                        usuarioLogin.Name = usuario.Nombre_de_usuario;
                        usuarioLogin.Pass = usuario.Pass;
                        return RedirectToAction("LogIn","Login",usuarioLogin);
                    }
                    ModelState.AddModelError(nameof(ViewUsuarioAdd.PassControl), "Las contraseñas no son iguales");
                }else{
                    ModelState.AddModelError(nameof(ViewUsuarioAdd.Nombre_de_usuario), "El Nombre de Usuario ya está en uso.");
                }
            }
            return View(user);
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        try{
            if(HttpContext.Session.GetString("Rol")==null) return RedirectToRoute(new{controller = "Login", action="Index"});
            Usuario usuario = _usuarioRepository.GetUsuario(id);
            if(usuario!=null){
                if(isAdmin() && HttpContext.Session.GetInt32("Id")!=id){
                    var usuarioUpdate = new ViewUsuarioUpdateRol(usuario);
                    return View("ModificarUsuarioRol",usuarioUpdate);
                }else if(isAdmin() || HttpContext.Session.GetInt32("Id")==id){
                    var usuarioUpdate = new ViewUsuarioUpdate(usuario);
                    return View(usuarioUpdate);
                }
            }
            return RedirectToAction("Index");
        }catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult ModificarUsuario(int id, ViewUsuarioUpdate user)
    {
        try
        {
            if(HttpContext.Session.GetString("Rol")==null) return RedirectToRoute(new{controller = "Login", action="Index"});
            if(ModelState.IsValid){
                Usuario usuario = _usuarioRepository.GetUsuario(id);
                if(usuario!=null){
                    if(isAdmin() || HttpContext.Session.GetInt32("Id")==id){
                        if(usuario.Pass!=user.Pass){
                            ModelState.AddModelError(nameof(ViewUsuarioAdd.Pass), "La Contraseña es incorrecta.");
                        }else{
                            _usuarioRepository.UpdateUsuario(id,user);
                            HttpContext.Session.SetString("Imagen", user.Imagen);
                            return RedirectToAction("Index");
                        }
                    }
                }else{
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    public IActionResult ModificarUsuarioRol(int id, ViewUsuarioUpdateRol user)
    {
        try
        {
            if(HttpContext.Session.GetString("Rol")==null) return RedirectToRoute(new{controller = "Login", action="Index"});
            if(ModelState.IsValid){
                Usuario usuario = _usuarioRepository.GetUsuario(id);
                if(usuario!=null){
                    if(isAdmin()){
                        _usuarioRepository.UpdateUsuarioRol(id,user.Rol);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    public IActionResult EliminarUsuario(int id){
        try{
            if(isAdmin() && _usuarioRepository.GetCountUsuarioAdmin()==1 && HttpContext.Session.GetInt32("Id") == id){
                TempData["AlertMessage"] = "No se puede eliminar el unico administrador.";
                return RedirectToAction("Index");
            }
            if(isAdmin() || HttpContext.Session.GetInt32("Id") == id){
                var Tableros = _tableroRepository.GetAllTablerosForUser(id);
                foreach (var t in Tableros)
                {
                    var tareas = _tareaRepository.GetAllTareasForTablero(t.Id);
                    foreach (var tarea in tareas)
                    {
                        _tareaRepository.DeleteTarea(tarea);
                    }
                    _tableroRepository.DeleteTablero(t.Id);
                }
                _usuarioRepository.DeleteUsuario(id);
                cargaTableros();
                if(HttpContext.Session.GetInt32("Id") == id){
                    return RedirectToRoute(new{controller = "Login", action="LogOut"});
                }else{
                    return RedirectToAction("Index");
                }
            }else{
                return RedirectToRoute(new{controller = "Login", action="Index"});
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    private bool isAdmin(){
        if(HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador"){
            return true;
        }
        return false;
    }
    private void cargaTableros(){
        var tableros = _tableroRepository.GetAllTableros();
        List<ViewTableroNav> viewTableroNavs = new List<ViewTableroNav>();
        foreach (var t in tableros)
        {
            var VMTablero = new ViewTableroNav(t);
            viewTableroNavs.Add(VMTablero);
        }
        HttpContext.Session.SetObjectAsJson("Tableros", viewTableroNavs);
    }
}
