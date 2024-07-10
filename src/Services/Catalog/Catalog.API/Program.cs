
var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// add servicves to ther container

builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly); // scanea validators en todo el proyecto y los inyecta

builder.Services.AddCarterWithAssemblies(assembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
