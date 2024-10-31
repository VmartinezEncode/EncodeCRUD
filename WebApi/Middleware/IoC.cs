using DataAccess.Generic;

namespace WebApi.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
//Debido a que se pueden tener multiples accesos al mismo tiempo al mismo dato, esto puede traer proplemas de inconsistencia de datos.