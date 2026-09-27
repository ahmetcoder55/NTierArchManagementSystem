using DataAccess.Concrete.Context;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using WebApi.ActionFilters;

namespace WebApi.Extensions
{
    public static class WebApiExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
        public static void ConfigureJWT(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

            });
        }

        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
        }

        public static void ConfigureRateLimitting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddFixedWindowLimiter(policyName: "FixedPolicy", fixedOptions =>
                {
                    fixedOptions.PermitLimit = 3;                        // İzin verilen maks istek sayısı
                    fixedOptions.Window = TimeSpan.FromSeconds(60);      // 60 saniyelik zaman dilimi
                    fixedOptions.QueueLimit = 0;                         // Kuyrukta bekleyebilecek maks istek
                });
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo // Name çakışmasını önlemek için tam yol
                {
                    Title = "Developer Ahmet",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Developer Ahmet",
                        Url = new Uri("https://ahmetcoder55.github.io/static-blog/index.html")
                    }
                });

                // 1. DÜZELTME: JWT Bearer Şemasını eksiksiz tanımlıyoruz
                s.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Lütfen kutucuğa sadece token değerinizi yapıştırın. (Otomatik Bearer eklenir veya 'Bearer {token}' formatını deneyin)",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http, // ApiKey yerine Http seçilmesi Bearer için en doğrusudur
                    Scheme = "Bearer",
                    BearerFormat = "JWT" // Sistem bunun bir JWT olduğunu bilmeli
                });

                // 2. DÜZELTME: Güvenlik gereksinimini klasik ve kararlı nesne yapısıyla bağlıyoruz
                s.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
                });

            });
        }

    }
}
