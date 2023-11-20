using tl2_tp10_2023_Ragahe10.Models;

namespace tl2_tp10_2023_Ragahe10;

public class UsuarioLogin{
    private string nombre;
    private string pass;
    public string Nombre { get => nombre; set => nombre = value; }
    public string Pass { get => pass; set => pass = value; }

    public UsuarioLogin(Usuario usuario)
    {
        nombre = usuario.NombreDeUsuario;
        pass = usuario.Pass;
    }

    public UsuarioLogin(){}
}