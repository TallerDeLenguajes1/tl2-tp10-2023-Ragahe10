namespace Proyecto.Models;
using System.ComponentModel.DataAnnotations;
using Proyecto.ViewModels;

public class ViewTableroVer{
    public ViewTableroVer(ViewTableroInfo tablero, List<ViewTareaInfo> tareas)
    {
        Id = tablero.Id;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
        Usuario_propietario = tablero.Usuario_propietario;
        Imagen_usuario = tablero.Imagen_usuario;
        Tareas = tareas;
    }

    public int Id {get;}
    public string Nombre {get;}
    public string Descripcion {get;}
    public string Usuario_propietario {get;}
    public string Imagen_usuario {get;}
    public List<ViewTareaInfo> Tareas {get;}
}