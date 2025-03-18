using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Abstract;
using Repositories.Concrete;
using Repositories.Context;
using Services;
using Services.Abstract;
using System.Text;

namespace WebApiProject.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection"),b=>b.MigrationsAssembly("WebApiProject"));
            });
        }

        public static void ConfigureManagerRepository(this IServiceCollection service)
        {
            service.AddScoped<IRepositoryManager, RepositoryManager>();
            service.AddScoped<IBookRepository, BookRepository>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IBookservice, BookManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IAuthendicationService, AuthendicationManager>();
        }
        public static void  ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
        }
        public static void ConfigureJWT(this IServiceCollection services,IConfiguration configuration)
        {//Tokeini doğrulamak için gerekli parametreleri eşleştirdik
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,//Bu keyi kim ürettiyse bunu doğrula
                    ValidateAudience = true,//geçerli bir alıcımı değil mi doğrula
                    ValidateLifetime = true,//geçerliliğini doğrula
                    ValidateIssuerSigningKey = true,//anahtarı doğrulamak için kullanılır
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                }
            );
        }

    }
}
