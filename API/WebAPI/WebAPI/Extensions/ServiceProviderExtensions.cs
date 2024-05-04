namespace WebAPI.Extensions
{
    using CodeFirst;
    using CodeFirst.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceProviderExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
