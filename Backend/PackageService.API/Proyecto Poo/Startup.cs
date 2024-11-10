using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Helpers;
using Proyecto_Poo.Service;
using Proyecto_Poo.Service.Interface;
using System;
using System.Text;

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
            services.AddHttpContextAccessor();

            var name = Configuration.GetConnectionString("DefaultConnection");

            // Configuración de la conexión a la base de datos
            services.AddDbContext<PackageServiceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configuración de Identity
            services.AddIdentity<UserEntity, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<PackageServiceDbContext>()
                .AddDefaultTokenProviders();

            // Configuración de autenticación y JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            // Configuración de servicios personalizados
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPackageService, PackageService>();
            services.AddTransient<IAudtiService,AuditService>();


            // Configuración de AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // Configuración de CORS
            services.AddCors(opt =>
            {
                var allowURLs = Configuration.GetSection("AllowURLS").Get<string[]>();
                opt.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins(allowURLs)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials());
            });
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

            // Habilitar CORS y autenticación
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
