namespace tl2_tp10_2023_Ragahe10.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaUpdate{
    public int Id {get;set;}
    public int IdTablero {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public string Color {get;set;}
    public EstadoTarea Estado {get;set;}
    public int IdUsuarioAsignado {get;set;}
    private List<Tablero> tableros;
    private List<Usuario> usuarios;

    public ViewTareaUpdate()
    {
    }

    public ViewTareaUpdate(Tarea tarea, List<Tablero> tableros, List<Usuario> usuarios){
        Id = tarea.Id;
        IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        this.tableros = tableros;
        this.usuarios = usuarios;
    }

    public List<Tablero> Tableros { get => tableros; set => tableros = value; }
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
}
