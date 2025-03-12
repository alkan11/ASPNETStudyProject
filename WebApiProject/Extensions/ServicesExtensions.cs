using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;
using Repositories.Concrete;
using Repositories.Context;
using Services;
using Services.Abstract;

namespace WebApiProject.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection"));
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

    }
}
