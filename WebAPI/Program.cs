
using Application.Behaviors;
using Application.Common.Security;
using Application.Interfaces;
using FluentValidation;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Reflection;
using System.Text;
using WebAPI.Middleware;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // --- 1. CONTROLLERS & JSON AYARLARI ---
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Sonsuz döngüleri (Cycle) engellemek için
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            // --- 2. SWAGGER & JWT TANIMLAMASI ---
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Club Management API", Version = "v1" });

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Lütfen sadece token metnini buraya yapýþtýrýn.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        System.Array.Empty<string>()
                    }
                });
            });

            // --- 3. VERÝTABANI & REPOSITORY KAYITLARI ---
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // --- 4. MEDIATR & FLUENTVALIDATION & BEHAVIORS ---
            var assembly = typeof(Application.AssemblyReference).Assembly;
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            builder.Services.AddValidatorsFromAssembly(assembly);
            builder.Services.AddAutoMapper(assembly);

            
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PermissionBehavior<,>));

            // --- 5. AUTHENTICATION (KÝMLÝK) & AUTHORIZATION (YETKÝ) ---
            builder.Services.AddScoped<JwtProvider>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };

                    // Hata ayýklama loglarý (Debug penceresinde görünür)
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context => {
                            System.Diagnostics.Debug.WriteLine("!!! JWT HATASI: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnChallenge = context => {
                            System.Diagnostics.Debug.WriteLine("!!! UYARI: Sunucuya token gelmedi veya format hatalý.");
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization(); 

            // --- 6. CORS & LOGGING ---
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll", policy => {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
            builder.Host.UseSerilog();
        
            var app = builder.Build();

            // --- PIPELINE SIRALAMASI (BURASI HAYATÝ ÖNEMDEDÝR) ---

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();

            
            app.UseCors("AllowAll");

            app.UseAuthentication(); 
            app.UseAuthorization();  

            app.MapControllers();

            app.Run();
        }
    }
}