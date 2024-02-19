namespace Proyecto.ViewModels;
using Proyecto.Models;
using System.ComponentModel.DataAnnotations;
public class ViewUsuarioLogin{
    [Required]
    [StringLength(100)]
    public string Name{get;set;}
    [Required]
    [StringLength(20)]
    public string Pass{get;set;}
}