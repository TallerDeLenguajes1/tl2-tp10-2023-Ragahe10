using System.Diagnostics;
using System.Formats.Tar;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository _tareaRepository;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
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
                ViewTareaListado tareas = new ViewTareaListado(_tareaRepository.GetAllTareas(),_usuarioRepository.GetAllUsuarios(),_tableroRepository.GetAllTableros());
                return View(tareas);
            }else{
                ViewTareaListado tareas = new ViewTareaListado(_tareaRepository.GetAllTareas().FindAll(t => t.IdUsuarioAsignado ==  HttpContext.Session.GetInt32("id")),_usuarioRepository.GetAllUsuarios(),_tableroRepository.GetAllTableros());
                return View("TareaOperador",tareas);
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        try{
            if(isAdmin()){
                return View(new ViewTareaAdd(new Tarea(),_tableroRepository.GetAllTableros(), _usuarioRepository.GetAllUsuarios()));
                //return View(new Tarea());
            }
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult CrearTarea(ViewTareaAdd t)
    {
        try{
            if(ModelState.IsValid){
                var tarea = new Tarea(t);
                if(isAdmin()){
                    _tareaRepository.AddTarea(tarea);
                    return RedirectToAction("Index");
                }
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }
            return RedirectToAction("CrearTarea");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        try{
            var tarea = _tareaRepository.GetAllTareas().FirstOrDefault(t => t.Id == id);
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                return View(new ViewTareaUpdate(tarea,_tableroRepository.GetAllTableros(), _usuarioRepository.GetAllUsuarios()));
            }else{
                if(HttpContext.Session.GetInt32("id")==tarea.IdUsuarioAsignado){
                    return View("ModificarTareaOperador",new ViewTareaUpdate(tarea,_tableroRepository.GetAllTableros(), _usuarioRepository.GetAllUsuarios()));
                    //return RedirectToAction("ModificarUsuario",usuario);
                }else{
                    return RedirectToAction("Index");
                }
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, ViewTareaUpdate t)
    {   
        try{
            if(ModelState.IsValid){
                Tarea tarea = new Tarea(t);
                if(HttpContext.Session.GetString("Rol")==null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
                }else if(isAdmin()){
                    _tareaRepository.UpdateTarea(id,tarea);
                }else{
                    if(HttpContext.Session.GetInt32("id")==id){
                        _tareaRepository.UpdateTarea(id,tarea);
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("ModificarTarea");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    public IActionResult EliminarTarea(int id)
    {
        try{
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                    _tareaRepository.DeleteTarea(id);
            }else{
                var tarea = _tareaRepository.GetTarea(id);
                if(tarea!=null && HttpContext.Session.GetInt32("id")==tarea.IdUsuarioAsignado){
                    _tareaRepository.DeleteTarea(id);
                }
            }
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
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
