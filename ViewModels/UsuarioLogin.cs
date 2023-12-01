namespace tl2_tp10_2023_Ragahe10.Models;
using System.ComponentModel.DataAnnotations;

public class UsuarioLogin{
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)] 
    public string Nombre {get; set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(20)]
    public string Pass {get; set;}
    public UsuarioLogin(Usuario usuario)
    {
        Nombre = usuario.NombreDeUsuario;
        Pass = usuario.Pass;
    }

    public UsuarioLogin(){}
}