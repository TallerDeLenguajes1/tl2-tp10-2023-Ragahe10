namespace Proyecto.Repository;
using System.Data.SQLite;
using Proyecto.Models;
using Proyecto.ViewModels;

public class TableroRepository : ITableroRepository{
    private string CadenaDeConexion;
    public TableroRepository(string CadenaDeConexion)
    {
        this.CadenaDeConexion = CadenaDeConexion;
    }
    public void AddTablero(Tablero tablero){
        var query = @"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@id_usuario_propietario, @nombre, @descripcion);";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", tablero.Id_usuario_propietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public Tablero GetTablero(int id){
        var query = @"SELECT * FROM Tablero WHERE id=@id;";
        Tablero tablero = null;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                if(reader.Read()){
                    tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    if(reader["descripcion"]==DBNull.Value){
                        tablero.Descripcion = "Sin Descripcion";
                    }else{
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }
            }
            connection.Close();
        }
        return tablero;
    }
    public List<Tablero> GetAllTableros(){
        var query = @"SELECT * FROM Tablero;";
        List<Tablero> tableros = new List<Tablero>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    if(reader["descripcion"]==DBNull.Value){
                        tablero.Descripcion = "Sin Descripcion";
                    }else{
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                    tableros.Add(tablero);
                }
            }
            connection.Close();
        }
        return tableros;
    }
    public List<Tablero> GetAllTablerosForUser(int id){
        var query = @"SELECT * FROM Tablero WHERE id_usuario_propietario = @id;";
        List<Tablero> tableros = new List<Tablero>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    if(reader["descripcion"]==DBNull.Value){
                        tablero.Descripcion = "Sin Descripcion";
                    }else{
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                    tableros.Add(tablero);
                }
            }
            connection.Close();
        }
        return tableros;
    }
    public ViewTableroInfo GetTableroView(int id){
        var query = @"SELECT t.id, nombre_de_usuario as propietario, imagen, nombre, descripcion FROM Tablero t LEFT JOIN Usuario u ON t.id_usuario_propietario = u.id WHERE t.id = @id;";
        ViewTableroInfo tablero = null;
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    tablero = new ViewTableroInfo();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    var propietario = reader["propietario"];
                    if(propietario!=DBNull.Value){
                        tablero.Usuario_propietario = propietario.ToString();
                    }else{
                        tablero.Usuario_propietario = "Sin Propietario";
                    }
                    tablero.Imagen_usuario = reader["imagen"].ToString();
                    tablero.Nombre = reader["nombre"].ToString();
                    if(reader["descripcion"]==DBNull.Value){
                        tablero.Descripcion = "Sin Descripcion";
                    }else{
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }
            }
            connection.Close();
        }
        return tablero;
    }
    public List<ViewTableroInfo> GetAllTablerosView(){
        var query = @"SELECT t.id, nombre_de_usuario as propietario, nombre, descripcion FROM Tablero t LEFT JOIN Usuario u ON t.id_usuario_propietario = u.id;";
        List<ViewTableroInfo> tableros = new List<ViewTableroInfo>();
        using (SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tablero = new ViewTableroInfo();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    var propietario = reader["propietario"];
                    if(propietario!=DBNull.Value){
                        tablero.Usuario_propietario = propietario.ToString();
                    }else{
                        tablero.Usuario_propietario = "Sin Propietario";
                    }
                    tablero.Nombre = reader["nombre"].ToString();
                    if(reader["descripcion"]==DBNull.Value){
                        tablero.Descripcion = "Sin Descripcion";
                    }else{
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                    tableros.Add(tablero);
                }
            }
            connection.Close();
        }
        return tableros;
    }
    public void UpdateTablero(int id, Tablero tablero){
        var query = @"UPDATE Tablero SET id_usuario_propietario = @id_usuario_propietario, nombre = @nombre, descripcion = @descripcion WHERE id = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario",tablero.Id_usuario_propietario));
            command.Parameters.Add(new SQLiteParameter("@nombre",tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion",tablero.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void DeleteTablero(int id){
        var query = @"DELETE FROM Tablero WHERE id = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(CadenaDeConexion)){
            var command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}