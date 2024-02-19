namespace Proyecto.ViewModels;
using Proyecto.Models;
public class ViewUsuarioListado{
    public ViewUsuarioListado(List<Usuario> usuarios)
    {
        ListaDeUsuarios = new List<ViewUsuarioInfo>();
        foreach(var u in usuarios){
            var usuarioInfo = new ViewUsuarioInfo(u);
            ListaDeUsuarios.Add(usuarioInfo);
        }
    }

    public List<ViewUsuarioInfo> ListaDeUsuarios{get;}

}