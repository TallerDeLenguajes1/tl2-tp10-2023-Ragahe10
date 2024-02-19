namespace Proyecto.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaInfo{
    private int id;
    private string nombre;
    private string descripcion;
    private string color;
    private EstadoTarea estado;
    private string usuarioAsignado;
    private string imagen;


    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public string UsuarioAsignado { get => usuarioAsignado; set => usuarioAsignado = value; }
    public string Imagen { get => imagen; set => imagen = value; }
    
}
