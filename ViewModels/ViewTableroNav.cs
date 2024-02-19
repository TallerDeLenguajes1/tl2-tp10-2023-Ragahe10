namespace Proyecto.ViewModels;
using Proyecto.Models;

public class ViewTableroNav{
    public int Id {get; set;}
    public string Nombre {get; set;}
    public ViewTableroNav(Tablero tablero){
        Id = tablero.Id; 
        Nombre = tablero.Nombre; 
    }

    public ViewTableroNav()
    {
    }
}