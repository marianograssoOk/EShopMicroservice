var builder = WebApplication.CreateBuilder(args);

// add servicves to ther container

var app = builder.Build();

// Configure the http request pipeline

app.Run();
