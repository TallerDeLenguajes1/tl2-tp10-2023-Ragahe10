using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace tl2_tp09_2023_Ragahe10;


public class TableroRepository : ITableroRepository {
    private string cadenaConexion = "Data Source=DataBase/kanban.db;Cache=Shared";
    public void AddTablero(Tablero tablero){
        var query = @"INSERT INTO Tablero (id_usuario_propietario,nombre,descripcion) VALUES (@idUsuario, @nombreT, @descripcion);";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombreT", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
    public void UpdateUsuario(int idTablero, Tablero tablero){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE Tablero SET id_usuario_propietario = @idUsuario, nombre = @nombre, descripcion = @descripcion WHERE id_tablero = @idTablero;";
            command.Parameters.Add(new SQLiteParameter("@IdUsuario", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public Tablero GetTablero(int idTablero){
        var query = @"SELECT * FROM Tablero WHERE id_tablero = @idTablero;";
        Tablero tablero = new Tablero();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@idTablero",idTablero));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }
            connection.Close();
        }
        return tablero;
    }
    public List<Tablero> GetAllTableros(){
        var query = @"SELECT * FROM Tablero;";
        var tableros = new List<Tablero>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(query,connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    tableros.Add(tablero);
                }
            }
            connection.Close();
        }
        return tableros;
    }
    public List<Tablero> GetAllTablerosForUsuario(int idUsuario){
        var query = @"SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario;";
        var tableros = new List<Tablero>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    tableros.Add(tablero);
                }
            }
            connection.Close();
        }
        return tableros;
    }
    public void DeleteTablero(int idTablero){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Tablero WHERE id_tablero = @idTablero;";
            command.Parameters.Add(new SQLiteParameter("@idTablero",idTablero));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}