
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RSTech.Data;
using RSTech.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//DI Dbcontext
// builder.Services.AddDbContext<SongContext>(opt =>
//     opt.UseInMemoryDatabase("Songs"));
// builder.Services.AddDbContext<ArtistContext>(opt =>
//     opt.UseInMemoryDatabase("Artists"));

//DI
builder.Services.AddDbContext<SongContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RSConString")));
builder.Services.AddDbContext<ArtistContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RSConString")));
builder.Services.AddScoped<EfCoreSongRepository>();
builder.Services.AddScoped<EfCoreArtistRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
