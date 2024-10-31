using Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Configuration;
using WebApi.Middleware;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Environment.GetEnvironmentVariable("db_connection_string");
            if(connectionString == null)
            {
                connectionString = Configuration.GetConnectionString("DefaultConnection");
            }
            Console.WriteLine(connectionString);
            services.AddDbContext<WebApiDbContext>(options => options.UseMySQL(connectionString));
            services.AddAuthorization();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
            IoC.AddDependency(services);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI Encode");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            /*app.Use(async (context, next) =>
            {
                // Verifica si la solicitud es a una ruta específica, por ejemplo, "/api"
                if (context.Request.Path == "/index.html")
                {
                    // Redirige a la raíz "/"
                    context.Response.Redirect("/");
                    return;
                }
                await next();
            });*/

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
