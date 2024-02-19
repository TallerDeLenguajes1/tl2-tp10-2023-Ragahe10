namespace Proyecto.ViewModels;
using Proyecto.Models;
public class ViewUsuarioInfo{
    public ViewUsuarioInfo(Usuario usuario)
    {
        Id = usuario.Id;
        Nombre_de_usuario = usuario.Nombre_de_usuario;
        Rol = usuario.Rol;
        Imagen = usuario.Imagen;
    }

    public int Id{get;}
    public string Nombre_de_usuario{get;}
    public string Rol{get;}
    public string Imagen{get;}


}