namespace tl2_tp10_2023_Ragahe10.Models;

public class ViewTableroAdd {

    public int Id {get;set;}
    public int IdUsuarioPropietario {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public List<Usuario> Usuarios {get;set;}
    public ViewTableroAdd()
    {
    }
    public ViewTableroAdd(List<Usuario> usuarios)
    {
        Usuarios = usuarios;
    }
}
