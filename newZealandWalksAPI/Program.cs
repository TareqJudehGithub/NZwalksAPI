using Microsoft.EntityFrameworkCore;
using newZealandWalksAPI.Data;
using newZealandWalksAPI.Mappings;
using newZealandWalksAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// NZWalks DB Context connection
builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("NZWalksDBConnectionString")));

// Repositories DI
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AutoMapperProfiles)));

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
