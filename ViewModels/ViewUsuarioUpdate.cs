namespace tl2_tp10_2023_Ragahe10.Models;
using System.ComponentModel.DataAnnotations;

public class ViewUsuarioUpdate{
    public int Id;
    [Required (ErrorMessage ="Este campo es requerido")]
    [StringLength(100)]
    public string NombreDeUsuario;
    [Required (ErrorMessage ="Este campo es requerido")]
    [StringLength(50)]
    public string Rol;
    [Required (ErrorMessage ="Este campo es requerido")]
    [StringLength(20)]
    public string Pass;

    public ViewUsuarioUpdate()
    {
    }

    public ViewUsuarioUpdate(Usuario usuario)
    {
        Id = usuario.Id;
        NombreDeUsuario = usuario.NombreDeUsuario;
        Rol = usuario.Rol;
        Pass = usuario.Pass;
    }    
}