namespace tl2_tp09_2023_Ragahe10;

public interface ITableroRepository {
    public void AddTablero(Tablero tablero);
    public void UpdateUsuario(int idTablero, Tablero tablero);
    public Tablero GetTablero(int idTablero);
    public List<Tablero> GetAllTableros();
    public List<Tablero> GetAllTablerosForUsuario(int idUsuario);
    public void DeleteTablero(int idTablero);
}