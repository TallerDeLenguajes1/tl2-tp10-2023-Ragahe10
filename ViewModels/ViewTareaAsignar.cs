namespace Proyecto.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaAsignar{
    public string Nombre {get;set;}
    public string Color {get;set;}
    public int IdUsuarioAsignado {get;set;}
    public int IdTablero {get;set;}
    public int Id {get;set;}
    private List<Usuario> usuarios;

    public ViewTareaAsignar()
    {
    }

    public ViewTareaAsignar(Tarea tarea, List<Usuario> usuarios){
        Id = tarea.Id;
        IdTablero = tarea.Id_tablero;
        Nombre = tarea.Nombre;
        Color = tarea.Color;
        IdUsuarioAsignado = tarea.Id_usuario_asignado;
        this.usuarios = usuarios;
    }
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
}
