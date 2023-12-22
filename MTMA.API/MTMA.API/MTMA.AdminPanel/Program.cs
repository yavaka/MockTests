using MTMA.AdminPanel.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAdminPanelServices(builder.Configuration);

var app = builder.Build();

app.ConfigureAdminPanel();

app.Run();
