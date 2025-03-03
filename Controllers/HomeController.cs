﻿using System.Diagnostics;
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
        try{
            cargaTableros();
            return View();
        }catch (Exception ex) {
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

