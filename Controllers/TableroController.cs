﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository tableroRepository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Tablero> tableros = tableroRepository.GetAllTableros();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View(new Tablero());
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
}
