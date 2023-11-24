namespace tl2_tp10_2023_Ragahe10.Models;
public enum EstadoTarea{
    Ideas, 
    ToDo, 
    Doing, 
    Review, 
    Done
}
public class Tarea{
    public Tarea()
    {
    }

    public Tarea(ViewTareaAdd t)
    {
        Id = 0;
        IdTablero = t.IdTablero;
        Nombre = t.Nombre;
        Descripcion = t.Descripcion;
        Color = t.Color;
        Estado = t.Estado;
        IdUsuarioAsignado = t.IdUsuarioAsignado;
    }
    public Tarea(ViewTareaUpdate t)
    {
        Id = t.Id;
        IdTablero = t.IdTablero;
        Nombre = t.Nombre;
        Descripcion = t.Descripcion;
        Color = t.Color;
        Estado = t.Estado;
        IdUsuarioAsignado = t.IdUsuarioAsignado;
    }

    public int Id {get;set;}
    public int IdTablero {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public string Color {get;set;}
    public EstadoTarea Estado {get;set;}
    public int IdUsuarioAsignado {get;set;}
    
}