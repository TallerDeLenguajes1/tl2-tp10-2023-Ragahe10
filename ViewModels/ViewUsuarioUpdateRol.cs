namespace Proyecto.ViewModels;
using Proyecto.Models;
using System.ComponentModel.DataAnnotations;

public class ViewUsuarioUpdateRol{
    public string[] Roles = {"Administrador", "Operador"};
    public int Id {get;}
    public string Nombre_de_usuario {get;}

    public ViewUsuarioUpdateRol()
    {
    }
    public ViewUsuarioUpdateRol(Usuario usuario)
    {
        Id = usuario.Id;
        Nombre_de_usuario = usuario.Nombre_de_usuario;
        Rol = usuario.Rol;
    }

    [Required (ErrorMessage ="Campo requerido")]
    [StringLength (50)]
    public string Rol{get;set;}
}