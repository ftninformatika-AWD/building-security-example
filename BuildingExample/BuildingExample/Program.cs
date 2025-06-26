using BuildingExample.Models;
using BuildingExample.Repositories;
using BuildingExample.Services;
using BuildingExample.Settings;
using BuildingExample.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Napredno konfigurisanje Swagger-a
builder.Services.AddSwaggerGen(c =>
{
    // Navođenje verzije API dokumentacije
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Building Example API", Version = "v1" });

    // Definisanje JWT Bearer autentifikacije
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert JWT token"
    });

    // Primena Bearer autentifikacije
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});

// Registracija Identity-a
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// Definisanje uslova koje lozinka mora da ispuni
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;                // Mora da sadrži bar jednu cifru
    options.Password.RequireLowercase = true;            // Mora da sadrži bar jedno malo slovo
    options.Password.RequireUppercase = true;            // Mora da sadrži bar jedno veliko slovo
    options.Password.RequireNonAlphanumeric = true;      // Mora da sadrži bar jedan specijalan (nealfanumerički) karakter (npr. !, @, #)
    options.Password.RequiredLength = 8;                 // Minimalna dužina lozinke je 8 karaktera
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,            // Provera validnosti adrese aplikacije koja izdaje token
        ValidateAudience = true,          // Provera validnosti adrese aplikacije koja koristi token
        ValidateLifetime = true,          // Provera da li je istekao token
        ClockSkew = TimeSpan.Zero,        // Onemogucava vremensku toleranciju pri validaciji tokena, pa token postaje nevazeci tacno u trenutku isteka vazenja 
        ValidateIssuerSigningKey = true,  // Provera da li je validan ključ za generisanje tokena (koji se koristi i prilikom čitanja tokena)
        ValidIssuer = builder.Configuration["Jwt:Issuer"],      // Navodi se adresa aplikacije koja izdaje token na osnovu koje se vrši provera (učitava se iz appsettings.json)
        ValidAudience = builder.Configuration["Jwt:Audience"],  // Navodi se adresa aplikacije koja koristi token na osnovu koje se vrši provera (učitava se iz appsettings.json)
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),  // Navodi se ključ za proveru tokena (učitava se iz appsettings.json)

        // Ovo omogućava ASP.NET-u da koristi ClaimTypes.Role za [Authorize(Roles = "...")]
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddAuthorization();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();

builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
