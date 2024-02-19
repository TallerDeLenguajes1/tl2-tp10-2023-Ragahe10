using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Repository;
using Proyecto.ViewModels;

namespace Proyecto.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;
    private ITareaRepository _tareaRepository;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index()
    {
        try
        {
            if(HttpContext.Session.GetString("User")==null) return RedirectToRoute(new {controller = "Login", action = "Index"});
            ViewTableroListado tableroListado = new ViewTableroListado(_tableroRepository.GetAllTablerosView());
            return View(tableroListado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult CrearTablero()
    {
        try{
            if(HttpContext.Session.GetString("User") != null) return View(new ViewTableroAdd(_usuarioRepository.GetAllUsuarios()));
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult CrearTablero(ViewTableroAdd viewTablero)
    {
        try{
            if(ModelState.IsValid){
                if(HttpContext.Session.GetString("User") == null)return RedirectToRoute(new{controller = "Login", action = "Index"});
                var tablero = new Tablero(viewTablero);
                var usuario = _usuarioRepository.GetUsuario(tablero.Id_usuario_propietario);
                if(usuario!=null){
                    _tableroRepository.AddTablero(tablero);
                    return RedirectToAction("Index");
                }else{
                    ModelState.AddModelError(nameof(ViewTableroAdd.IdUsuarioPropietario), "El usuario es no existe.");
                }
            }
            var usuarios = _usuarioRepository.GetAllUsuarios();
            viewTablero.Usuarios = usuarios;
            return View("CrearTablero", viewTablero);
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {   
        try{
            var tablero = _tableroRepository.GetTablero(id);
            if(tablero != null){
                if(HttpContext.Session.GetString("Rol") == null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
                }else if(esPropietario(id)){
                    ViewTableroUpdate viewTablero = new ViewTableroUpdate (tablero);
                    return View(viewTablero);
                }
            }
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
           return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult ModificarTablero(int id, ViewTableroUpdate viewTablero)
    {
        try{
            if(ModelState.IsValid){
                var t = _tableroRepository.GetTablero(id);
                if(t != null){
                    if(HttpContext.Session.GetString("Rol") == null){
                        return RedirectToRoute(new{controller = "Login", action = "Index"});
                    }else if(esPropietario(id)){
                        Tablero tablero = new Tablero(viewTablero);
                        _tableroRepository.UpdateTablero(id,tablero);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("ModificarTablero",viewTablero);
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult VerTablero(int id)
    {   
        try{
            var tableroInfo = _tableroRepository.GetTableroView(id);
            if(tableroInfo != null){
                if(HttpContext.Session.GetString("Rol") == null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
                }else{
                    var tareasInfo = _tareaRepository.GetAllTareasForTablero(id);
                    var viewTableroVer = new ViewTableroVer(tableroInfo,tareasInfo);
                    return View(viewTableroVer);
                }
            }
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
           return RedirectToAction("Error");
        }
    }
    public IActionResult EliminarTablero(int id)
    {
        try{
            if(HttpContext.Session.GetString("Rol") == null)return RedirectToRoute(new{controller = "Login", action = "Index"});
            if(isAdmin() || esPropietario(id)){
                _tableroRepository.DeleteTablero(id);
            }
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    private bool isAdmin(){
        if(HttpContext.Session.GetString("Rol") == "Administrador"){
            return true;
        }
        return false;
    }
    private bool esPropietario(int id){
        var tablero = _tableroRepository.GetTablero(id);
        if(tablero != null && tablero.Id_usuario_propietario == HttpContext.Session.GetInt32("Id")){
            return true;
        }
        return false;
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
}
