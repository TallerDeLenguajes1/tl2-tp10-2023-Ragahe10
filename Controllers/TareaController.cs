using System.Diagnostics;
using System.Formats.Tar;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepository;
    private ITableroRepository tableroRepository;
    private IUsuarioRepository usuarioRepository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
        tableroRepository = new TableroRepository();
        usuarioRepository = new UsuarioRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            ViewListarTareas tareas = new ViewListarTareas(tareaRepository.GetAllTareas(),usuarioRepository.GetAllUsuarios(),tableroRepository.GetAllTableros());
            return View(tareas);
        }else{
            ViewListarTareas tareas = new ViewListarTareas(tareaRepository.GetAllTareas().FindAll(t => t.IdUsuarioAsignado ==  HttpContext.Session.GetInt32("id")),usuarioRepository.GetAllUsuarios(),tableroRepository.GetAllTableros());
            return View("TareaOperador",tareas);
        }
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        if(isAdmin()){
            return View(new ViewTareaAdd(new Tarea(),tableroRepository.GetAllTableros(), usuarioRepository.GetAllUsuarios()));
            //return View(new Tarea());
        }
        return RedirectToRoute(new{controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult CrearTarea(ViewTareaAdd t)
    {
        var tarea = new Tarea(t);
        if(isAdmin()){
            tareaRepository.AddTarea(tarea);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new{controller = "Login", action = "Index"});
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        var tarea = tareaRepository.GetAllTareas().FirstOrDefault(t => t.Id == id);
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            return View(new ViewTareaUpdate(tarea,tableroRepository.GetAllTableros(), usuarioRepository.GetAllUsuarios()));
        }else{
            if(HttpContext.Session.GetInt32("id")==tarea.IdUsuarioAsignado){
                return View("ModificarTareaOperador",new ViewTareaUpdate(tarea,tableroRepository.GetAllTableros(), usuarioRepository.GetAllUsuarios()));
                //return RedirectToAction("ModificarUsuario",usuario);
            }else{
                return RedirectToAction("Index");
            }
        }
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, ViewTareaUpdate t)
    {   
        Tarea tarea = new Tarea(t);
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            tareaRepository.UpdateTarea(id,tarea);
        }else{
            if(HttpContext.Session.GetInt32("id")==id){
                tareaRepository.UpdateTarea(id,tarea);
            }
        }
        return RedirectToAction("Index");
    }
    public IActionResult EliminarTarea(int id)
    {
        if(HttpContext.Session.GetInt32("Rol")!=null){
            tareaRepository.DeleteTarea(id);
            return RedirectToAction("index");
        }
        return RedirectToRoute(new{controller = "Login", action = "Index"});
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
