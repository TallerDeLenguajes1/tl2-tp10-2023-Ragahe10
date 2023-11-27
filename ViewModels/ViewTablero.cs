namespace tl2_tp10_2023_Ragahe10.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTablero {

    public int Id {get;set;}
    public string UsuarioPropietario {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public ViewTablero()
    {
    }
    public ViewTablero(Tablero tablero, Usuario usuario)
    {
        Id = tablero.Id;
        UsuarioPropietario = usuario.NombreDeUsuario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
}
public class ViewTableroUpdate {

    public int Id {get;set;}
    public int IdUsuarioPropietario {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public List<Usuario> Usuarios {get;set;}
    public ViewTableroUpdate()
    {
    }
    public ViewTableroUpdate(Tablero tablero, List<Usuario> usuarios)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
        Usuarios = usuarios;
    }
}