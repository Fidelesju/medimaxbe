using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using MediMax.Application.Configurations;
using MediMax.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    builder.Services.AddRazorPages();

    builder.Services.AddControllers();
    //Connection to database
    string mySqlConnection =
        builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<MediMaxDbContext>(options =>
        options.UseMySql(mySqlConnection,
            ServerVersion.AutoDetect(mySqlConnection)));
    builder.Services.RegisterService();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerConfiguration();
    builder.Services.AddJwtConfiguration(builder.Configuration);

var app = builder.Build();
    app.MapControllers();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthConfiguration();
    app.MapRazorPages();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MediMax v1");
    });


    app.Run();
