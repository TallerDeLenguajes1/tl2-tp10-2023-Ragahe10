using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Repository;
using Proyecto.ViewModels;

namespace Proyecto.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ITableroRepository _tableroRepository;

    public HomeController(ILogger<HomeController> logger, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
    }

    public IActionResult Index()
    {
        cargaInicial();
        return View();
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
    private void cargaInicial(){
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

