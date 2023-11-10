using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace tl2_tp09_2023_Ragahe10;


public class UsuarioRepository : IUsuarioRepository {
    private string cadenaConexion = "Data Source=DataBase/kanban.db;Cache=Shared";
    public void AddUsuario(Usuario usuario){
        var query = @"INSERT INTO Usuario (nombre_de_usuario) VALUES (@nombre_de_usuario);";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            connection.Open();
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void UpdateUsuario(int idUsuario, Usuario usuario){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE Usuario SET nombre_de_usuario = @nombre WHERE id = @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@nombre",usuario.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public List<Usuario> GetAllUsuarios(){
        var query = @"SELECT * FROM Usuario;";
        List<Usuario> usuarios = new List<Usuario>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(query,connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuarios.Add(usuario);
                }
            }
        }
        return usuarios;
    }
    public Usuario GetUsuario(int idUsuario){
        var query = @"SELECT * FROM Usuario WHERE id = @idUsuario;";
        Usuario usuario = new Usuario();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                }
            }
            connection.Close();
        }
        return usuario;
    }
    public void DeleteUsuario(int idUsuario){
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Usuario WHERE id = @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}