using Microsoft.EntityFrameworkCore;
using MusicPortalWebApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MusicClubContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();


var app = builder.Build();

app.UseStaticFiles();

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
