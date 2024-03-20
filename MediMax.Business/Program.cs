using MediMax.Business.CoreServices;
using System.Configuration;
using MediMax.Data.Models;
using MediMax.Data.ApplicationModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona os serviços ao contêiner.
        builder.Services.AddRazorPages();
        builder.Services.AddControllers();

        // Adiciona a configuração JWT
        AddJwtConfiguration(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configura o pipeline de solicitação HTTP.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // Configura a autenticação e autorização
        UseAuthConfiguration(app);

        app.MapRazorPages();

        app.Run();
    }

    private static void AddJwtConfiguration(IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);

        AppSettings appSettings = appSettingsSection.Get<AppSettings>();
        byte[] key = Encoding.ASCII.GetBytes(appSettings.Segredo);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.Authority = "https://localhost:5000/swagger/index.html";
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.RequireHttpsMetadata = false;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false
            };
        });
    }

    private static void UseAuthConfiguration(IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
