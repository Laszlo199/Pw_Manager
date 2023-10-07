using Microsoft.EntityFrameworkCore;
using Pw_Security.Db;
using Pw_Security.Helper;
using Pw_Security.IRepository;
using Pw_Security.IServices;
using Pw_Security.Repositories;
using Pw_Security.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthHelper, AuthHelper>();
builder.Services.AddScoped<SecuritySeeder>();

builder.Services.AddDbContext<SecurityContext>(options => { options.UseSqlite("Data Source = auth.db"); });
builder.Services.AddTransient<SecuritySeeder>();




var app = builder.Build();

AuthSeeder(app);

void AuthSeeder(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SecuritySeeder>();
        service.SeedDevelopment();
    }
}

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