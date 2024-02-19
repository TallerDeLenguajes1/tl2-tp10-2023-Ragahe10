namespace Proyecto.Models;
using Proyecto.ViewModels;
public class Usuario{
    public string[] Roles = {"Administrador", "Operador"};
    private int id;
    private string nombre_de_usuario;
    private string pass;
    private string rol;
    private string imagen;

    public Usuario()
    {
    }

    public Usuario(ViewUsuarioAdd usuarioAdd)
    {
        nombre_de_usuario = usuarioAdd.Nombre_de_usuario;
        pass = usuarioAdd.Pass;
        imagen = usuarioAdd.Imagen;
    }

    public int Id { get => id; set => id = value; }
    public string Nombre_de_usuario { get => nombre_de_usuario; set => nombre_de_usuario = value; }
    public string Pass { get => pass; set => pass = value; }
    public string Rol { get => rol; set => rol = value; }
    public string Imagen { get => imagen; set => imagen = value; }
}