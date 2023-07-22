using MTMA.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAPIServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
