using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RentalyaAPI.Models;
using RentalyaAPI.Configurations;
using RentalyaAPI.Data;
using RentalyaAPI.Services;

namespace RentalyaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // JWT Configuration
            builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

            // MongoDB Configuration
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));
            builder.Services.AddSingleton<ApplicationDbContext>();

            // Identity yerine sadece Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    // ... mevcut JWT ayarları ...
                });

            // Auth servisini ekleyelim
            builder.Services.AddScoped<IAuthService, AuthService>();

            // CORS Configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("ReactPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}