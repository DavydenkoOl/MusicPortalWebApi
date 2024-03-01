using Microsoft.EntityFrameworkCore;
using MusicPortalWebApi.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();


// Add services to the container.
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MusicClubContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();


var app = builder.Build();

app.UseCors(builder => builder.WithOrigins("https://localhost:7074")
                            .AllowAnyHeader()
                            .AllowAnyMethod());

app.UseCors(builder => builder.WithOrigins("http://localhost:5244")
                            .AllowAnyHeader()
                            .AllowAnyMethod());


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
