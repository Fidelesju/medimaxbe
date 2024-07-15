using MediMax.Application.Configurations;
using MediMax.Business.Mappers;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Configura��o de Serilog para gravar logs no banco de dados
Log.Logger = new LoggerConfiguration()
    .WriteTo.MySQL(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        tableName: "log", // Nome da tabela de logs
        restrictedToMinimumLevel: LogEventLevel.Information) // N�vel m�nimo de logs a serem gravados
    .CreateLogger();

// Outros servi�os do aplicativo
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDbContext<MediMaxDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
builder.Services.RegisterService();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


var app = builder.Build();
app.MapControllers();

// Configura��es de ambiente de produ��o
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Outros middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthConfiguration();
app.MapRazorPages();
app.UseRouting();
app.UseMiddleware<LoggingMiddleware>(); // Adiciona o middleware de logs

// Configura��es de Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MediMax v1");
});

//Configura��es SignalR
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationHub");
    // Outros mapeamentos de endpoints
});
// Inicia o aplicativo
app.Run();
