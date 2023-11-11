using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Tarea> tareas = tareaRepository.GetAllTareas();
        return View(tareas);
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        return View(new Tarea());
    }
    [HttpPost]
    public IActionResult CrearTablero(Tarea tarea)
    {
        tareaRepository.AddTarea(tarea);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        var tablero = tareaRepository.GetAllTareas().FirstOrDefault(u => u.Id == id);
        return View(tablero);
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, Tarea tarea)
    {
        tareaRepository.UpdateTarea(id,tarea);
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
}
