namespace Proyecto.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaAdd{
    [Required (ErrorMessage ="este campo es requerido")]
    public int IdTablero {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string Nombre {get;set;}
    [StringLength(2000)]
    public string Descripcion {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string Color {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    public EstadoTarea Estado {get;set;}
    public int IdUsuarioAsignado {get;set;}
    private List<Usuario> usuarios;
    private List<Tablero> tableros;

    public ViewTareaAdd()
    {
    }

    public ViewTareaAdd(Tarea tarea, List<Tablero> tableros, List<Usuario> usuarios){
        IdTablero = tarea.Id_tablero;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.Id_usuario_asignado;
        this.usuarios = usuarios;
        this.tableros = tableros;
    }
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
    public List<Tablero> Tableros { get => tableros; set => tableros = value; }
}
