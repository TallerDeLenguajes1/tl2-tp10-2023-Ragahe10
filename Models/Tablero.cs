namespace tl2_tp10_2023_Ragahe10.Models;
public class Tablero{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string descripcion;

    public Tablero()
    {
    }
    public Tablero(ViewTableroAdd tableroAdd)
    {
        id = tableroAdd.Id;
        idUsuarioPropietario = tableroAdd.IdUsuarioPropietario;
        nombre = tableroAdd.Nombre;
        descripcion = tableroAdd.Descripcion;
    }
    public Tablero(ViewTableroUpdate tableroAdd)
    {
        id = tableroAdd.Id;
        idUsuarioPropietario = tableroAdd.IdUsuarioPropietario;
        nombre = tableroAdd.Nombre;
        descripcion = tableroAdd.Descripcion;
    }

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
}