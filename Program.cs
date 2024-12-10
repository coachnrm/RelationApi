using Microsoft.EntityFrameworkCore;
using RelationApi.Controllers;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite("Data Source=Relations.db"));
builder.Services.AddControllers()
    .AddJsonOptions(x=> x.JsonSerializerOptions.ReferenceHandler =
System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapScalarApiReference();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
