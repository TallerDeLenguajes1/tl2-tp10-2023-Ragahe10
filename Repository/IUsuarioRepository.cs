namespace tl2_tp09_2023_Ragahe10;

public interface IUsuarioRepository {
    public void AddUsuario(Usuario usuario);
    public void UpdateUsuario(int idUsuario, Usuario usuario);
    public List<Usuario> GetAllUsuarios();
    public Usuario GetUsuario(int idUsuario);
    public void DeleteUsuario(int idUsuario);
}