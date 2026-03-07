using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // for Swagger authentication

using newZealandWalksAPI.Data;
using newZealandWalksAPI.Mappings;
using newZealandWalksAPI.Repositories;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.Xml;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Image upload - IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

// Add authorization to Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = " NZ Walks API",
        Version = "v1"
    });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name =  JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// NZWalks DBContext connection
builder.Services
    .AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(
    builder.Configuration.GetConnectionString("NZWalksDBConnectionString")
    ));

// NZWalks Auth DBContext connection
builder.Services
    .AddDbContext<NZWalksAuthDbContext>(options =>
    options.UseSqlServer(
    builder.Configuration.GetConnectionString("NZWalksAuthConnetionStrong")
    ));

// Repositories DI 
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AutoMapperProfiles)));

// Identity service
builder.Services
    .AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalksAuthDbContext>()
    .AddDefaultTokenProviders();

// Password identity options for new users creation
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


// JWT authentication service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration[key: "Jwt:Issuer"],
        ValidAudiences = new[] { builder.Configuration[key: "Jwt:Audience"] },
        IssuerSigningKey = new SymmetricSecurityKey(
            key: Encoding.UTF8.GetBytes(builder.Configuration[key: "Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adding authentication  for JWT
app.UseAuthentication();

app.UseAuthorization();

// Use static files
app.UseStaticFiles(new StaticFileOptions
{
    // Map to https://localhost:7150/Images
    FileProvider = new PhysicalFileProvider(Path
    .Combine(Directory
    .GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
}
    );

app.MapControllers();

app.Run();
