namespace tl2_tp09_2023_Ragahe10;

public interface ITareaRepository{
    public void AddTarea(Tarea tarea);
    public void UpdateTarea(int idTarea, Tarea tarea);
    public void UpdateEstadoTarea(int id, EstadoTarea estado);
    public Tarea GetTarea(int idTarea);
    public List<Tarea> GetAllTareas();
    public void DeleteTarea(int idTarea);
    public void AssingTarea(int idUsuario, int idTarea);
}