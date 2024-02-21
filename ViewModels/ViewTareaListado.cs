namespace Proyecto.ViewModels;
using Proyecto.Models;
public class ViewTareaListado{
    private List<ViewTarea> tareas;
    public List<ViewTarea> Tareas { get => tareas; set => tareas = value; }
    public List<Usuario> Usuarios {get; set;}
    public ViewTareaListado(List<ViewTarea> tareas, List<Usuario> usuarios){
        this.tareas = tareas;
        Usuarios = usuarios;
    }
}