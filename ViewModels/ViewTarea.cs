namespace tl2_tp10_2023_Ragahe10.Models;

public class ViewListarTarea{
    private int id;
    private string nombreTablero;
    private string nombre;
    private string descripcion;
    private string color;
    private EstadoTarea estado;
    private string usuarioAsignado;


    public int Id { get => id; set => id = value; }
    public string NombreTablero { get => nombreTablero; set => nombreTablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public string UsuarioAsignado { get => usuarioAsignado; set => usuarioAsignado = value; }
    public ViewListarTarea(Tarea tarea, Usuario usuario, Tablero tablero)
    {
        this.id = tarea.Id;
        if(tablero == null){
            nombreTablero = "Sin Tablero";
        }else{
            this.nombreTablero = tablero.Nombre;
        }
        this.nombre = tarea.Nombre;
        this.descripcion = tarea.Descripcion;
        this.color = tarea.Color;
        this.estado = tarea.Estado;
        if(usuario == null){
            usuarioAsignado = "Sin Asignar";
        }else{
            this.usuarioAsignado = usuario.NombreDeUsuario;
        }
    }
}
public class ViewListarTareas{
    private List<ViewListarTarea> vTareas;
    public List<ViewListarTarea> VTareas { get => vTareas; set => vTareas = value; }
    public ViewListarTareas(List<Tarea> tareas, List<Usuario> usuarios, List<Tablero> tableros){
        vTareas = new List<ViewListarTarea>();
        foreach(var tarea in tareas){
            var usuario = usuarios.FirstOrDefault(u => u.Id == tarea.IdUsuarioAsignado);
            var tablero = tableros.FirstOrDefault(t => t.Id == tarea.IdTablero);
            vTareas.Add(new ViewListarTarea(tarea,usuario,tablero));
        }
    }
}
public class ViewTareaInfo{
    private Tarea tarea;
    private List<Tablero> tableros;
    private List<Usuario> usuarios;

    public ViewTareaInfo()
    {
    }

    public ViewTareaInfo(Tarea tarea, List<Tablero> tableros, List<Usuario> usuarios){
        this.tarea = tarea;
        this.tableros = tableros;
        this.usuarios = usuarios;
    }

    public Tarea Tarea { get => tarea; set => tarea = value; }
    public List<Tablero> Tableros { get => tableros; set => tableros = value; }
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
}