using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PARCIAL1A.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Inyección por dependencia del string de conexion al contexto
builder.Services.AddDbContext<Parcial1AContext>(options => 
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("PARCIAL1ADbConnection")
            )
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
