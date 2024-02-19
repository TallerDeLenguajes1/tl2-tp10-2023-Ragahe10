namespace Proyecto.Repository;
using Proyecto.Models;
using Proyecto.ViewModels;
public interface ITareaRepository {
    public void AddTarea(Tarea tarea);
    public Tarea GetTarea(int id);
    public List<Tarea> GetAllTareas();
    public List<ViewTareaInfo> GetAllTareasForTablero(int id);
    public List<ViewTarea> GetAllTareasView();
    public void UpdateTarea(int id, Tarea tarea);
    public void DeleteTarea(int id);
}