using FatecFoodAPI.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MySQL");

builder.Services.AddDbContext<FatecFoodAPIContext>(ops =>
{
    ops.UseMySql(connectionString, ServerVersion.Parse("8.9.23-mysql"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(ops =>
{
    ops.AllowAnyOrigin();
    ops.AllowAnyMethod();
    ops.AllowAnyHeader();
});

app.MapControllers();

app.Run();
