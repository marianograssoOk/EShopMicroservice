using BuildingBlocks.Extentions;

var builder = WebApplication.CreateBuilder(args);

// add servicves to ther container
builder.Services.AddCarterWithAssemblies(typeof(Program).Assembly);
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});


var app = builder.Build();

// Configure the http request pipeline

app.MapCarter();

app.Run();
