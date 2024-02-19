// Ejecuto en cosola: dotnet new mvc, para crear el proyecto mvc
// dotnet watch run 
// Luego: dotnet add package System.Data.SQLite, incluyo librería para base de datos SQLite
// Creo la carpeta de la base de datos, pego la base de datos ahí
// Agrego en el .csproj 
/*<ItemGroup>
    <None Update="{NombreDeLaCarpeta}\{NombreDeLaBaseDeDatos}.db">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
  </ItemGroup>*/

// Creo los modelos de datos
// Agrego en appsettings.json la variable gloval de la cadena de conexion
/*
"ConnectionStrings": {
    "SqliteConexion":"Data Source=DataBase/kanban.db;Cache=Shared"
  }
*/
// Agrego en el program el -1- primera inyeccion de dependencias
// Crear Interfaces de repositorios con sus metodos.
// Crear Repositorios heredados de una interfas e implementar los metodos
/* GUIA GRAL PARA MEETODO DEL REPOSITORIO:
    -1- Defino la consulta (var query = @"{Consulta SQL}")
    -2- Creo la vble. connection a partir de la cadena de conexion (using(var connection = new SQLiteConnection({CadenaDeConexion})){}).
    -3- Defino la vble command a partir de la query y connection (var command = vew SQLiteCommand(query,connection)).
    -4- Cargo los parametros de la query, en caso de haber. (command.Parameters.Add(new SQLiteParameter("{parametro}", valor)))
    -5- Abro la conexion (connection.Open())
    -6- Ejecuto el comando (command.ExecuteNonQuery() para modificaciones y SQLiteReader reader = command.ExecuteReader())
    -7- Cierro la conexion (connection.Close())
*/
// Incluyo el paquete para sesiones
//dotnet add package Microsoft.AspNetCore.Session
// agrego en el program el -2-

// agrego en el program el -3- segunda inyeccion de dependencias para interfaces de repositorios
// incluyo el espacio
using Proyecto.Repository;

// Creo los controllers con su respectiva carpeta detro de View (Ej: Controlador: {nombre}Controller, CarpetaVista: {nombre})
// Creo viewModel para usar en los EndPoints y aplico validaciones en los viewModels que llevan info a los controllers.
// Creo los EndPoints usando los viewModels, controlo si el modelo es valido en caso de ser necesario (ModelState.IsValid)
// Por cada uno de ser necesario creo una vista .cshtml, dentro de la carpeta Vista de su respectivo controller

// En las vistas que tengan formularios agrego al final: @section Scripts{<partial name="_ValidationScriptsPartial"/>}
// para aplicar las validaciones de los modelos
// Y abajo de cada input :<span asp-validation-for="@Model.Nombre_de_usuario" class="text-danger"></span>

// Agrego controles de inicio de sesion (opcional) y Captura de exepciones (try-cach)

// Agrego registro de logueo
// agrego en el en controllers y layout, controles de inicio de sesion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// -1-
var CadenaDeConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();
builder.Services.AddSingleton<string>(CadenaDeConexion);
// -FIN 1-
// -3-
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();
builder.Services.AddScoped<ITableroRepository,TableroRepository>();
builder.Services.AddScoped<ITareaRepository,TareaRepository>();
// -FIN 3-
// -2 primera parte-
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(300);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
// -FIN 2-
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// -2 segunda parte-
app.UseSession();
// -FIN 2-
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
