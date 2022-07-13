using ImprimeEtiqueta;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(builder.Configuration.GetValue<int>("Porta"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/ImprimeUmaEtiqueta", ([FromBody] string etiqueta, IConfiguration configuration) =>
{
    return Impressao.Etiqueta(etiqueta, configuration, 1);
})
.WithName("PostImprimeUmaEtiqueta");

app.MapPost("/ImprimeDuasEtiqueta", ([FromBody] string etiqueta, IConfiguration configuration) =>
{
    return Impressao.Etiqueta(etiqueta, configuration, 2);
})
.WithName("PostImprimeDuasEtiqueta");
app.Run();