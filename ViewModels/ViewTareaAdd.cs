namespace tl2_tp10_2023_Ragahe10.Models;
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
    private List<Tablero> tableros;
    private List<Usuario> usuarios;

    public ViewTareaAdd()
    {
    }

    public ViewTareaAdd(Tarea tarea, List<Tablero> tableros, List<Usuario> usuarios){
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
