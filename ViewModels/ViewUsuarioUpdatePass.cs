namespace Proyecto.ViewModels;
using Proyecto.Models;
using System.ComponentModel.DataAnnotations;

public class ViewUsuarioUpdate{
    public int Id {get; set;}
    public string Nombre_de_usuario {get; set;}

    public ViewUsuarioUpdate()
    {
    }
    public ViewUsuarioUpdate(Usuario usuario)
    {
        Id = usuario.Id;
        Nombre_de_usuario = usuario.Nombre_de_usuario;
        Pass = usuario.Pass;
        Imagen = usuario.Imagen;
    }
    [Required (ErrorMessage ="Campo requerido")]
    [StringLength (20)]
    public string NewPass{get;set;}
    [Required (ErrorMessage ="Campo requerido")]
    [StringLength (20)]
    public string Pass{get;set;}
    public string Imagen{get;set;}
}