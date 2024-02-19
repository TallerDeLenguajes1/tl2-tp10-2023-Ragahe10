namespace Proyecto.Models;

public class Tablero{
    private int id;
    private int id_usuario_propietario;
    private string nombre;
    private string descripcion;

    public Tablero()
    {
    }
    public Tablero(ViewTableroAdd tableroAdd)
    {
        id = tableroAdd.Id;
        id_usuario_propietario = tableroAdd.IdUsuarioPropietario;
        nombre = tableroAdd.Nombre;
        descripcion = tableroAdd.Descripcion;
    }
    public Tablero(ViewTableroUpdate tableroAdd)
    {
        id = tableroAdd.Id;
        id_usuario_propietario = tableroAdd.IdUsuarioPropietario;
        nombre = tableroAdd.Nombre;
        descripcion = tableroAdd.Descripcion;
    }
    public int Id { get => id; set => id = value; }
    public int Id_usuario_propietario { get => id_usuario_propietario; set => id_usuario_propietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    
}