namespace Proyecto.ViewModels;
using Proyecto.Models;

public class ViewTableroListado{
    public ViewTableroListado(List<ViewTableroInfo> listaDeTableros)
    {
        ListaDeTableros = listaDeTableros;
    }

    public List<ViewTableroInfo> ListaDeTableros {get;}
}