namespace tl2_tp10_2023_Ragahe10.Models;
using System.ComponentModel.DataAnnotations;

public class ViewUsuarioAdd{
    private int id;
    private string nombreDeUsuario;
    private string rol;
    private string pass;

    public ViewUsuarioAdd()
    {
    }

    public ViewUsuarioAdd(Usuario usuario)
    {
        id = usuario.Id;
        nombreDeUsuario = usuario.NombreDeUsuario;
        rol = usuario.Rol;
        pass = usuario.Pass;
    }

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Rol { get => rol; set => rol = value; }
    public string Pass { get => pass; set => pass = value; }
    
}