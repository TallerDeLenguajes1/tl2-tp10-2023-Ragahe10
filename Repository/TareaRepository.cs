namespace Proyecto.Repository;
using System.Data.SQLite;
using Proyecto.Models;
using Proyecto.ViewModels;

public class TareaRepository : ITareaRepository{
    private string CadenaDeConexion;
    public TareaRepository(string CadenaDeConexion)
    {
        this.CadenaDeConexion = CadenaDeConexion;
    }
    public void AddTarea(Tarea tarea){
        var query = @"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_usuario_asignado);";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.Id_tablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", tarea.Id_usuario_asignado));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public Tarea GetTarea(int id){
        var query = @"SELECT * FROM Tarea WHERE id=@id;";
        Tarea tarea = null;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                if(reader.Read()){
                    tarea = new Tarea();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    if(reader["descripcion"]==DBNull.Value){
                        tarea.Descripcion = "Sin Descripcion";
                    }else{
                        tarea.Descripcion = reader["descripcion"].ToString();
                    }
                    if(reader["color"]==DBNull.Value){
                        tarea.Color = "Sin Color";
                    }else{
                        tarea.Color = reader["color"].ToString();
                    }
                    if(reader["id_usuario_asignado"]==DBNull.Value){
                        tarea.Id_usuario_asignado = 0;
                    }else{
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                }
            }
            connection.Close();
        }
        return tarea;
    }
    public List<Tarea> GetAllTareas(){
        var query = @"SELECT * FROM Tarea;";
        List<Tarea> tareas = new List<Tarea>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tarea = new Tarea();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    if(reader["descripcion"]==DBNull.Value){
                        tarea.Descripcion = "Sin Descripcion";
                    }else{
                        tarea.Descripcion = reader["descripcion"].ToString();
                    }
                    if(reader["color"]==DBNull.Value){
                        tarea.Color = "Sin Color";
                    }else{
                        tarea.Color = reader["color"].ToString();
                    }
                    if(reader["id_usuario_asignado"]==DBNull.Value){
                        tarea.Id_usuario_asignado = 0;
                    }else{
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                    tareas.Add(tarea);
                }
            }
            connection.Close();
        }
        return tareas;
    }
    public List<int> GetAllTareasForTablero(int id){
        var query = @"SELECT * FROM Tarea WHERE id_tablero = @id_tablero;";
        var id_tareas = new List<int>();
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("id_tablero",id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    int id_tarea = Convert.ToInt32(reader["id"]);
                    id_tareas.Add(id_tarea);
                }
            }
        }
        return id_tareas;
    }
    public List<ViewTareaInfo> GetAllTareasForTableroView(int id){
        var query = @"SELECT t.id, t.nombre, descripcion, color, estado, u.nombre_de_usuario as usuario_asignado, u.imagen FROM Tarea t LEFT JOIN Usuario u ON (t.id_usuario_asignado = u.id) WHERE id_tablero = @id_tablero;";
        List<ViewTareaInfo> tareas = new List<ViewTareaInfo>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("id_tablero",id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tarea = new ViewTareaInfo();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    if(reader["descripcion"]==DBNull.Value){
                        tarea.Descripcion = "Sin Descripcion";
                    }else{
                        tarea.Descripcion = reader["descripcion"].ToString();
                    }
                    if(reader["color"]==DBNull.Value){
                        tarea.Color = "Sin Color";
                    }else{
                        tarea.Color = reader["color"].ToString();
                    }
                    if(reader["imagen"]==DBNull.Value){
                        tarea.Imagen = "sinImagen.png";
                    }else{
                        tarea.Imagen = reader["imagen"].ToString();
                    }
                    if(reader["usuario_asignado"]==DBNull.Value){
                        tarea.UsuarioAsignado = "Sin asignar";
                    }else{
                        tarea.UsuarioAsignado = Convert.ToString(reader["usuario_asignado"]);
                    }
                    tareas.Add(tarea);
                }
            }
            connection.Close();
        }
        return tareas;
    }
    public List<ViewTarea> GetAllTareasView(){
        var query = @"SELECT t.id, t.nombre, t.descripcion, color, estado, u.nombre_de_usuario AS usuario_asignado, u.imagen, tl.id AS id_tablero, tl.nombre AS tablero, us.id as propietario_tablero FROM Tarea t LEFT JOIN Usuario u ON (t.id_usuario_asignado = u.id) INNER JOIN Tablero tl ON (t.id_tablero = tl.id) INNER JOIN Usuario us ON (tl.id_usuario_propietario = us.id);";
        List<ViewTarea> tareas = new List<ViewTarea>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tarea = new ViewTarea();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    if(reader["imagen"]==DBNull.Value){
                        tarea.Imagen = "sinImagen.png";
                    }else{
                        tarea.Imagen = reader["imagen"].ToString();
                    }
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    if(reader["descripcion"]==DBNull.Value){
                        tarea.Descripcion = "Sin Descripcion";
                    }else{
                        tarea.Descripcion = reader["descripcion"].ToString();
                    }
                    if(reader["color"]==DBNull.Value){
                        tarea.Color = "Sin Color";
                    }else{
                        tarea.Color = reader["color"].ToString();
                    }
                    if(reader["usuario_asignado"]==DBNull.Value){
                        tarea.Usuario_asignado = "Sin asignar";
                    }else{
                        tarea.Usuario_asignado = Convert.ToString(reader["usuario_asignado"]);
                    }
                    tarea.Tablero = reader["tablero"].ToString();
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Propietario_tablero = Convert.ToInt32(reader["propietario_tablero"]);

                    tareas.Add(tarea);
                }
            }
            connection.Close();
        }
        return tareas;
    }
    public void UpdateTarea(int id, Tarea tarea){
        var query = @"UPDATE Tarea SET id_tablero = @id_tablero, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @id_usuario_asignado WHERE id = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id_tablero",tarea.Id_tablero));
            command.Parameters.Add(new SQLiteParameter("@nombre",tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado",tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion",tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color",tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado",tarea.Id_usuario_asignado));
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void DeleteTarea(int id){
        var query = @"DELETE FROM Tarea WHERE id = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}