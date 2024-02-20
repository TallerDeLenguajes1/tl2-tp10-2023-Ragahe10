namespace Proyecto.Repository;
using Proyecto.Models;
using Proyecto.ViewModels;
public interface IUsuarioRepository{
    public void AddUsuario(Usuario usuario);
    public Usuario GetUsuario(int id);
    public int GetCountUsuarioAdmin();
    public Usuario GetUsuarioLogin(ViewUsuarioLogin user);
    public List<Usuario> GetAllUsuarios();
    public void UpdateUsuario(int id, ViewUsuarioUpdate viewUsuario);
    public void UpdateUsuarioRol(int id, string rol);
    public bool Existe(string nombre);
    public void DeleteUsuario(int id);
}