namespace Proyecto.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaUpdate{
    public int Id {get;set;}
    public string Nombre {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    public EstadoTarea Estado {get;set;}

    public ViewTareaUpdate()
    {
    }

    public ViewTareaUpdate(Tarea tarea){
        Id = tarea.Id;
        Nombre = tarea.Nombre;
        Estado = tarea.Estado;
    }
}