namespace tl2_tp10_2023_Ragahe10.Models;

public class ViewTableroListado{
    private List<ViewTablero> viewTableros;

    public List<ViewTablero> ViewTableros { get => viewTableros; set => viewTableros = value; }

    public ViewTableroListado(List<Tablero> tableros, List<Usuario> usuarios){
        viewTableros = new List<ViewTablero>();
        foreach (var t in tableros){
            foreach (var u in usuarios){
                if(t.IdUsuarioPropietario == u.Id){
                    var viewTablero = new ViewTablero(t,u);
                    viewTableros.Add(viewTablero);
                }
            }
        }
    }
}