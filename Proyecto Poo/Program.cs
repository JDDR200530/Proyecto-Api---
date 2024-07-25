using Proyecto_Poo;

var builder = WebApplication.CreateBuilder(args);


var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment);

// en este punto la aplicaion ya esta cargada y puede usar todo los servicios


app.Run();

