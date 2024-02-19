namespace Proyecto.ViewModels;
using Proyecto.Models;

public class ViewTarea{
    public int Id {get; set;}
    public string Nombre {get; set;}
    public string Descripcion {get; set;}
    public string Color {get; set;}
    public EstadoTarea Estado {get; set;}
    public string Usuario_asignado {get; set;}
    public string Imagen {get; set;}
    public string Tablero {get; set;}
    public int Id_tablero {get; set;}
}