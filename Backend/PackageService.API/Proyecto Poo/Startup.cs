using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Helpers;
using Proyecto_Poo.Service;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Add DbContext
            services.AddDbContext<PackageServiceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add custom services
            services.AddTransient<ICustomerService, CustomerService>(); // Cambiado de ICategoriesService a ICustomerService
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IOrderService, OrderService>(); // Cambiado de IPostsService a IOrderService
            services.AddTransient<IPackageService, PackageService>(); // Cambiado de IPostsService a IOrderService

            // Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile)); // Asegúrate de que AutoMapperProfile esté correctamente definido

            // Remove comment from Add custom services line if necessary
            // services.AddTransient<IAuthService, AuthService>(); // Remove this line if it's a comment causing error

            // Ensure correct configuration for AutoMapper, if needed
            // services.AddAutoMapper(typeof(Startup)); // Example if AutoMapperProfile is in the same assembly

            // Add any other services needed for your application
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
