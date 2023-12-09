using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try{
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                ViewTableroListado viewTableros = new ViewTableroListado(_tableroRepository.GetAllTableros(),_usuarioRepository.GetAllUsuarios());
                
                return View(viewTableros);
            }else{
                ViewTableroListado tableros = new ViewTableroListado(_tableroRepository.GetAllTableros().FindAll(t => t.IdUsuarioPropietario == HttpContext.Session.GetInt32("id")),_usuarioRepository.GetAllUsuarios());
                return View(tableros);
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        try{
            if(isAdmin()) return View(new ViewTableroAdd(_usuarioRepository.GetAllUsuarios()));
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
                if(isAdmin()){
                    var tablero = new Tablero(viewTablero);
                    _tableroRepository.AddTablero(tablero);
                    return RedirectToAction("Index");
                }
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }
            return RedirectToAction("CrearTablero");
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
                }else if(isAdmin()){
                    ViewTableroUpdate viewTablero = new ViewTableroUpdate (tablero, _usuarioRepository.GetAllUsuarios());
                    return View(viewTablero);
                }else{
                    if(HttpContext.Session.GetInt32("id")==tablero.IdUsuarioPropietario){
                        ViewTableroUpdate viewTablero = new ViewTableroUpdate (tablero, _usuarioRepository.GetAllUsuarios());
                        return View("ModificarTableroOperador",viewTablero);
                    }
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
                    }else if(isAdmin()){
                        Tablero tablero = new Tablero(viewTablero);
                        _tableroRepository.UpdateTablero(id,tablero);
                    }else{
                        if(HttpContext.Session.GetInt32("id")==t.IdUsuarioPropietario){
                            Tablero tablero = new Tablero(viewTablero);
                            _tableroRepository.UpdateTablero(id,tablero);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("ModificarTablero");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    public IActionResult EliminarTablero(int id)
    {
        try{
            if(HttpContext.Session.GetString("Rol") == null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else{
                if(isAdmin()){
                    _tableroRepository.DeleteTablero(id);
                }else{
                    var tablero = _tableroRepository.GetTablero(id);
                    if(tablero!=null && tablero.Id==HttpContext.Session.GetInt32("id")){
                        _tableroRepository.DeleteTablero(id);
                    }
                }
            }
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
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
}