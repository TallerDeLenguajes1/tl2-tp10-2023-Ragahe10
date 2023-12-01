namespace tl2_tp10_2023_Ragahe10.Models;
using System.ComponentModel.DataAnnotations;

public class ViewUsuarioAdd{
    public int Id {get; set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string NombreDeUsuario {get; set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(50)]
    public string Rol {get; set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(20)]
    public string Pass {get; set;}

    public ViewUsuarioAdd()
    {
    }

    public ViewUsuarioAdd(Usuario usuario)
    {
        Id = usuario.Id;
        NombreDeUsuario = usuario.NombreDeUsuario;
        Rol = usuario.Rol;
        Pass = usuario.Pass;
    }
}