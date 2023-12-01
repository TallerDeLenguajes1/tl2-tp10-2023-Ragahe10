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