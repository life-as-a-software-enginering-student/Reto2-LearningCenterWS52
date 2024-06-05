

using Microsoft.EntityFrameworkCore;
using Reto2.Domain;
using Reto2.Infrastructure.Contexts;
using Reto2.Infrastructure.Entities;
using Reto2.Infrastructure.Repositories;
using Reto2.Presentation.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependency injection*******
builder.Services.AddTransient<IClienteData, ClienteData>();
builder.Services.AddTransient<IClienteDomain, ClienteDomain>();

//Automapper******************
builder.Services.AddAutoMapper(
    typeof(ResquestToModels),
    typeof(ModelsToRequest),
    typeof(ModelsToResponse));


//Conexión a la base de datos MySQL*******
var connectionString = builder.Configuration.GetConnectionString("Reto2Connection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<Reto2Context>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    }
);

//-----------------------------------------------

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<Reto2Context>())
{
    context.Database.EnsureCreated();
}


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