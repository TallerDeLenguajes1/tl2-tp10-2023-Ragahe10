namespace tl2_tp10_2023_Ragahe10.Models;
 
public class ViewUsuarioListado{
    private List<ViewUsuario> viewUsuarios;
    public List<ViewUsuario> ViewUsuarios { get => viewUsuarios; set => viewUsuarios = value; }

    public ViewUsuarioListado(List<Usuario> usuarios)
    {
        viewUsuarios = new List<ViewUsuario>();
        foreach (var u in usuarios)
        {
            var viewUsuario = new ViewUsuario(u);
            viewUsuarios.Add(viewUsuario);
        }
    }

}