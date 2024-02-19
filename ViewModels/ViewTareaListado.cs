namespace Proyecto.ViewModels;
using Proyecto.Models;
public class ViewTareaListado{
    private List<ViewTarea> tareas;
    public List<ViewTarea> Tareas { get => tareas; set => tareas = value; }
    public ViewTareaListado(List<ViewTarea> tareas){
        this.tareas = tareas;
    }
}