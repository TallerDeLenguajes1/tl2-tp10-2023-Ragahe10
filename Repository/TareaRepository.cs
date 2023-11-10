using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;
namespace tl2_tp09_2023_Ragahe10;
public class TareaRepository : ITareaRepository{
    private string cadenaConexion = "Data Source=DataBase/kanban.db;Cache=Shared";
    public void AddTarea(Tarea tarea){
        var query = @"INSERT INTO Tarea (id_tablero,nombre,estado,descripcion,color) VALUES (@id_tablero,@nombre,@estado,@descripcion,@color);";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void UpdateTarea(int idTarea, Tarea tarea){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE Tarea SET id_tablero = @id_tablero, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color WHERE id = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void UpdateEstadoTarea(int idTarea, EstadoTarea estado){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE Tarea SET estado = @estado WHERE id = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("@estado", estado));
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public Tarea GetTarea(int idTarea){
        var query = @"SELECT * FROM Tarea WHERE id = @idTarea;";
        Tarea tarea = new Tarea();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea",idTarea));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"]==DBNull.Value){
                        tarea.IdUsuarioAsignado = 0;
                    }else{
                        tarea.IdUsuarioAsignado = (int)reader["id_usuario_asignado"];
                    }
                }
            }
            connection.Close();
        }
        return tarea;

    }
    public List<Tarea> GetAllTareas(){
        var query = @"SELECT * FROM Tarea;";
        var tareas = new List<Tarea>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(query,connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tarea = new Tarea();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt64(reader["estado"]);
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"]==DBNull.Value){
                        tarea.IdUsuarioAsignado = 0;
                    }else{
                        tarea.IdUsuarioAsignado = (int)reader["id_usuario_asignado"];
                    }
                    tareas.Add(tarea);
                }
            }
            connection.Close();
        }
        return tareas;
    }
    public void DeleteTarea(int idTarea){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Tarea WHERE id = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("@idTarea",idTarea));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void AssingTarea(int idUsuario, int idTarea){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE Tablero SET id_usuario_asignado = @idUsuario WHERE id = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}