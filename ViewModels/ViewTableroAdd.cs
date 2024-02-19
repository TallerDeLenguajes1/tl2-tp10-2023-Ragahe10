namespace Proyecto.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTableroAdd {

    public int Id {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    public int IdUsuarioPropietario {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string Nombre {get;set;}
    [StringLength(300)]
    public string Descripcion {get;set;}
    public List<Usuario> Usuarios {get;set;}
    public ViewTableroAdd()
    {
    }
    public ViewTableroAdd(List<Usuario> usuarios)
    {
        Usuarios = usuarios;
    }
}
