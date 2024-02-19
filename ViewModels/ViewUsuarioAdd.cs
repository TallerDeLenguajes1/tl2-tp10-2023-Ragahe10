using System.ComponentModel.DataAnnotations;
using Proyecto.Repository;

namespace Proyecto.ViewModels;
using Proyecto.Models;
public class ViewUsuarioAdd{
    [Required (ErrorMessage ="Campo requerido")]
    [StringLength (100)]
    public string Nombre_de_usuario{get;set;}
    [Required (ErrorMessage ="Campo requerido")]
    [StringLength (20)]
    public string Pass{get;set;}
    [Required (ErrorMessage ="Campo requerido")]
    [StringLength (20)]
    public string PassControl{get;set;}
    public string Imagen{get;set;}
}