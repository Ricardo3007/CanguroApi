using CanguroApi.Aplicattions;
using CanguroApi.Aplicattions.Contracts;
using CanguroApi.DataAccess.Context;
using CanguroApi.Domain.Services;
using CanguroApi.Domain.Services.Contracts;
using CanguroApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("ConnectionStringDb");
Console.WriteLine("Cadena de conexión: " + connectionString); // Imprime la cadena de conexión
builder.Services.AddDbContext<CanguroContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.PropertyNamingPolicy = null;
           options.JsonSerializerOptions.DictionaryKeyPolicy = null;
       });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CanguroApi", Version = "v1" });

        // Configura Swagger para incluir el token JWT en las solicitudes autorizadas
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    }
    );


builder.Services.AddTransient<ICanguroAppService, CanguroAppService>();
builder.Services.AddTransient<ICanguroDomain, CanguroDomain>();





// Load JWT settings from configuration
var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);


// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = jwtSettings.Issuer,
        //ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

// Add authorization
builder.Services.AddAuthorization();



var app = builder.Build();


app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:4200") // Reemplaza con la URL de tu aplicación Angular
          .AllowAnyHeader()
          .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
