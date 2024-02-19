using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Repository;
using Proyecto.ViewModels;

namespace Proyecto.Controllers;

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
            }else{
                ViewTareaListado listTareas = new ViewTareaListado(_tareaRepository.GetAllTareasView());
                return View(listTareas);
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult CrearTarea()
    {
        try{

            if(HttpContext.Session.GetString("User")!=null){
                var id_usuario = (int)HttpContext.Session.GetInt32("Id");
                var tableros = _tableroRepository.GetAllTablerosForUser(id_usuario);
                if(tableros.Count()>0){
                    return View(new ViewTareaAdd(new Tarea(),tableros, _usuarioRepository.GetAllUsuarios()));
                }else{
                    return View("ErrorDeCreacion");
                }
            }
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult CrearTarea(ViewTareaAdd t)
    {
        try{
            if(ModelState.IsValid){
                var tarea = new Tarea(t);
                if(HttpContext.Session.GetString("User")==null) return RedirectToRoute(new{controller = "Login", action = "Index"});
                var usuarios = _usuarioRepository.GetAllUsuarios();
                var coinsidencia = usuarios.FirstOrDefault(u=> u.Id == tarea.Id_usuario_asignado);
                if(_tableroRepository.GetTablero(tarea.Id_tablero)!=null && coinsidencia!=null){
                    _tareaRepository.AddTarea(tarea);
                }
                return RedirectToAction("Index");      
            }
            return RedirectToAction("CrearTarea");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
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
                if(tarea!=null && HttpContext.Session.GetInt32("id")==tarea.Id_usuario_asignado){
                    _tareaRepository.DeleteTarea(id);
                }
            }
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        try{
            var tarea = _tareaRepository.GetAllTareas().FirstOrDefault(t => t.Id == id);
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }
            if(esPropietario(id)){
                return View(new ViewTareaUpdate(tarea));
            }
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, ViewTareaUpdate t)
    {   
        try{
            if(ModelState.IsValid){
                if(HttpContext.Session.GetString("Rol")==null)return RedirectToRoute(new{controller = "Login", action = "Index"});
                var tarea = _tareaRepository.GetTarea(id);
                if(t.Estado == EstadoTarea.Ideas || t.Estado == EstadoTarea.Review || t.Estado == EstadoTarea.Doing || t.Estado == EstadoTarea.Done || t.Estado == EstadoTarea.ToDo){
                    tarea.Estado = t.Estado;   
                    if(esPropietario(tarea.Id)){
                        _tareaRepository.UpdateTarea(id,tarea);
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("ModificarTarea",id);
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
        var tarea = _tareaRepository.GetTarea(id);
        if(tarea != null && tarea.Id_usuario_asignado == HttpContext.Session.GetInt32("Id")){
            return true;
        }
        return false;
    }
}
