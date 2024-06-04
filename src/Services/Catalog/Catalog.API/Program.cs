using BuildingBlocks.Extentions;

var builder = WebApplication.CreateBuilder(args);

// add servicves to ther container
builder.Services.AddCarterWithAssemblies(typeof(Program).Assembly);
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();


var app = builder.Build();

// Configure the http request pipeline

app.MapCarter();

app.Run();
