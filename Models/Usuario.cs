namespace tl2_tp10_2023_Ragahe10.Models;

public class Usuario{
    private int id;
    private string nombreDeUsuario;
    private string rol;
    private string pass;

    public Usuario()
    {
    }

    public Usuario(ViewUsuarioAdd viewUsuarioAdd)
    {
        id = viewUsuarioAdd.Id;
        nombreDeUsuario = viewUsuarioAdd.NombreDeUsuario;
        rol = viewUsuarioAdd.Rol;
        pass = viewUsuarioAdd.Pass;

    }
    public Usuario(ViewUsuarioUpdate viewUsuarioUpdate)
    {
        id = viewUsuarioUpdate.Id;
        nombreDeUsuario = viewUsuarioUpdate.NombreDeUsuario;
        rol = viewUsuarioUpdate.Rol;
        pass = viewUsuarioUpdate.Pass;

    }

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Rol { get => rol; set => rol = value; }
    public string Pass { get => pass; set => pass = value; }
}