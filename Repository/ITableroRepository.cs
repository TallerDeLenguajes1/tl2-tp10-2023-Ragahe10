namespace Proyecto.Repository;
using Proyecto.Models;
using Proyecto.ViewModels;
public interface ITableroRepository {
    public void AddTablero(Tablero tablero);
    public Tablero GetTablero(int id);
    public List<Tablero> GetAllTableros();
    public List<Tablero> GetAllTablerosForUser(int id);
    public ViewTableroInfo GetTableroView(int id);
    public List<ViewTableroInfo> GetAllTablerosView();
    public void UpdateTablero(int id, Tablero tablero);
    public void DeleteTablero(int id);
}