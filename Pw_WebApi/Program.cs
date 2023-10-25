using System.Diagnostics;
using System.Text;
using Core.IServices;
using DataAcces;
using DataAcces.Repo;
using Domain.IRepository;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pw_Security.Db;
using Pw_Security.Helper;
using Pw_Security.IServices;
using Pw_Security.Repositories;
using Pw_Security.Services;
using Pw_WebApi.Exceptions;
using Pw_WebApi.Responses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pw_WebApi",
        Version = "v1"
    });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new[] {""}
        }
    });
});

// CORS config

builder.Services.AddCors(option =>
{
    option.AddPolicy("Pw_WebApi",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(authentificationOptions =>
    {
        authentificationOptions.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        authentificationOptions.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = false,
            //ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true
        };
    });

//Manager
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<ManagerSeeder>();

builder.Services.AddDbContext<PwManagerContext>(options => { options.UseSqlite("Data Source = manager.db"); });
builder.Services.AddTransient<ManagerSeeder>();

//Security
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthHelper, AuthHelper>();
builder.Services.AddScoped<SecuritySeeder>();

builder.Services.AddDbContext<SecurityContext>(options => { options.UseSqlite("Data Source = auth.db"); });
builder.Services.AddTransient<SecuritySeeder>();




var app = builder.Build();

AuthSeeder(app);
ManagerSeeder(app);

void AuthSeeder(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SecuritySeeder>();
        service.SeedDevelopment();
    }
}

void ManagerSeeder(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ManagerSeeder>();
        service.SeedDevelopment();
    }
}

// Error handling
app.UseExceptionHandler(a => a.Run(async context => {
    IExceptionHandlerPathFeature exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    Exception exception = exceptionHandlerPathFeature?.Error;
    // string trace = context.TraceIdentifier;
    string trace = Activity.Current?.Id ?? context.TraceIdentifier;
    int statusCode = context.Response.StatusCode;
    string type = "";
    if (exception is RestException restException) {
        context.Response.StatusCode = statusCode = (int)restException.Status;
        type = restException.Code ?? "";
    }

    await context.Response.WriteAsJsonAsync(new ErrorResponse(type, statusCode, trace, exception?.Message ?? ""));
}));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Pw_WebApi");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();