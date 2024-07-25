using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Context;

namespace Proyecto_Poo
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            var name = Configuration.GetConnectionString("DefaultConnection");
            //Add DbContext
            services.AddDbContext<PackageServiceDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // Add Custom Services
            //services.AddTransient<ICategoriesService, CategoriesService>();
            //services.AddTransient<ICategoriesService, CategoriesSQLService>();
            

            // Add AutoMapper
            //services.AddAutoMapper(typeof(AutoMapperProfile));

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
