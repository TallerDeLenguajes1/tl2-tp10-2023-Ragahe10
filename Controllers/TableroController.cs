using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository tableroRepository;
    private IUsuarioRepository usuarioRepository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
        usuarioRepository = new UsuarioRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {   
        if(HttpContext.Session.GetString("Rol")==null){
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }else if(isAdmin()){
            ViewTableroListado viewTableros = new ViewTableroListado(tableroRepository.GetAllTableros(),usuarioRepository.GetAllUsuarios());
            
            return View(viewTableros);
        }else{
            ViewTableroListado tableros = new ViewTableroListado(tableroRepository.GetAllTableros().FindAll(t => t.IdUsuarioPropietario == HttpContext.Session.GetInt32("id")),usuarioRepository.GetAllUsuarios());
            return View(tableros);
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        if(isAdmin()) return View(new ViewTableroAdd(usuarioRepository.GetAllUsuarios()));
        return RedirectToRoute(new{controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult CrearTablero(Tablero tablero)
    {
        tableroRepository.AddTablero(tablero);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        var tablero = tableroRepository.GetAllTableros().FirstOrDefault(u => u.Id == id);
        return View(tablero);
    }
    [HttpPost]
    public IActionResult ModificarTablero(int id, Tablero tablero)
    {
        tableroRepository.UpdateTablero(id,tablero);
        return RedirectToAction("Index");
    }
    public IActionResult EliminarTablero(int id)
    {
        tableroRepository.DeleteTablero(id);
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