namespace Proyecto.Models;
using System.ComponentModel.DataAnnotations;
public class ViewTableroUpdate {

    public int Id {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    public int IdUsuarioPropietario {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string Nombre {get;set;}
    [StringLength(300)]
    public string Descripcion {get;set;}
    public ViewTableroUpdate()
    {
    }
    public ViewTableroUpdate(Tablero tablero)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.Id_usuario_propietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
}