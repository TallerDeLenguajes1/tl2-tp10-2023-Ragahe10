using System.Diagnostics;
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
            return View(new ViewTareaInfo(new Tarea(),tableroRepository.GetAllTableros(), usuarioRepository.GetAllUsuarios()));
        }
        return RedirectToRoute(new{controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult CrearTarea(Tarea tarea)
    {
        if(isAdmin()){
            tareaRepository.AddTarea(tarea);;
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new{controller = "Login", action = "Index"});
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            var tablero = tareaRepository.GetAllTareas().FirstOrDefault(t => t.Id == id);
            return View(tablero);
        }else{
            if(HttpContext.Session.GetInt32("id")==id){
                var tablero = tareaRepository.GetAllTareas().FirstOrDefault(t => t.Id == id);;
                return View("ModificarTareaOperador",tablero);
                //return RedirectToAction("ModificarUsuario",usuario);
            }else{
                return RedirectToAction("Index");
            }
        }
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, Tarea tarea)
    {
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
        tareaRepository.DeleteTarea(id);
        return RedirectToAction("index");
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
