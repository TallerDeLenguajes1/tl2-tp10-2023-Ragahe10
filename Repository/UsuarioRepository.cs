namespace Proyecto.Repository;
using System.Data.SQLite;
using Proyecto.Models;
using Proyecto.ViewModels;

public class UsuarioRepository : IUsuarioRepository {
    private string CadenaDeConexion;
    private List<string> avateres = new List<string>{"p1.png","p2.png","p3.png","p4.png","p.png"};
    public UsuarioRepository(string CadenaDeConexion)
    {
        this.CadenaDeConexion = CadenaDeConexion;
    }
    public void AddUsuario(Usuario usuario){
        var query = @"INSERT INTO Usuario (nombre_de_usuario, pass, rol, imagen) VALUES (@nombre_de_usuario, @pass, @rol, @imagen);";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario",usuario.Nombre_de_usuario));
            command.Parameters.Add(new SQLiteParameter("@pass",usuario.Pass));
            command.Parameters.Add(new SQLiteParameter("@rol","Operador"));
            if(avateres.FirstOrDefault(a => a == usuario.Imagen)==null){
                command.Parameters.Add(new SQLiteParameter("@imagen","sinImagen.png"));
            }else{
                command.Parameters.Add(new SQLiteParameter("@imagen",usuario.Imagen));
            }
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public Usuario GetUsuario(int id){
        var query = @"SELECT * FROM Usuario WHERE id = @id;";
        Usuario usuario = null;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    usuario = new Usuario(); 
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    usuario.Rol = reader["rol"].ToString();
                    usuario.Pass = reader["pass"].ToString();
                    usuario.Imagen = reader["imagen"].ToString();
                }
            }
            connection.Close();
        }
        return usuario;
    }
    public int GetCountUsuarioAdmin(){
        var query = @"SELECT COUNT(id) as cant_admin FROM Usuario WHERE rol = @rol;";
        int cantAdmin = 0;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@rol","Administrador"));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                   cantAdmin = Convert.ToInt32(reader["cant_admin"]);
                }
            }
            connection.Close();
        }
        return cantAdmin;
    }
    public Usuario GetUsuarioLogin(ViewUsuarioLogin user){
        var query = @"SELECT * FROM Usuario WHERE nombre_de_usuario = @nombre_de_usuario AND pass = @pass;";
        Usuario usuario = null;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario",user.Name));
            command.Parameters.Add(new SQLiteParameter("@pass",user.Pass));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    usuario.Rol = reader["rol"].ToString();
                    usuario.Pass = reader["pass"].ToString();
                    usuario.Imagen = reader["imagen"].ToString();
                }
            }
            connection.Close();
        }
        return usuario;
    }
    public List<Usuario> GetAllUsuarios(){
        var query = @"SELECT * FROM Usuario;";
        List<Usuario> usuarios = new List<Usuario>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    usuario.Rol = reader["rol"].ToString();
                    usuario.Pass = reader["pass"].ToString();
                    usuario.Imagen = reader["imagen"].ToString();
                    usuarios.Add(usuario);
                }
            }
            connection.Close();
        }
        return usuarios;
    }
    public void UpdateUsuario(int id, ViewUsuarioUpdate viewUsuario){
        var query = @"UPDATE Usuario SET pass = @pass, imagen = @imagen WHERE id = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@pass",viewUsuario.NewPass));
            command.Parameters.Add(new SQLiteParameter("@imagen",viewUsuario.Imagen));
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void UpdateUsuarioRol(int id, string rol){
        var query = @"UPDATE Usuario SET rol = @rol WHERE id = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@rol",rol));
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public bool Existe(string nombre){
        var query = @"SELECT * FROM Usuario WHERE nombre_de_usuario = @nombre_de_usuario;";
        Usuario usuario = null;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario",nombre));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    usuario.Rol = reader["rol"].ToString();
                    usuario.Pass = reader["pass"].ToString();
                }
            }
            connection.Close();
        }
        return usuario!=null;
    }
    public void DeleteUsuario(int id){
        var query = @"DELETE FROM Usuario WHERE id = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}