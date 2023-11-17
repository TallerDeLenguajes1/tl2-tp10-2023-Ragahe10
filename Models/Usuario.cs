namespace tl2_tp10_2023_Ragahe10.Models;

public class Usuario{
    private int id;
    private string nombreDeUsuario;
    private string rol;
    private string pass;

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Rol { get => rol; set => rol = value; }
    public string Pass { get => pass; set => pass = value; }
}