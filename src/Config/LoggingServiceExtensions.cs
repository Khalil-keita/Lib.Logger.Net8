using Lib.Logger.Net8.src.Factory;
using Lib.Logger.Net8.src.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lib.Logger.Net8.src.Config
{
    /// <summary>
    /// Méthodes d'extension pour configurer les services de logging dans l'application
    /// </summary>
    public static class LoggingServiceCollectionExtensions
    {
        /// <summary>
        /// Ajoute les services de logging avec un logger par défaut
        /// </summary>
        /// <param name="services">Collection des services à configurer</param>
        /// <returns>Collection des services configurée avec le logging</returns>
        public static IServiceCollection AddAppLogging(this IServiceCollection services)
        {
            services.TryAddSingleton<ILoggerFactory, LoggerFactory>();
            services.TryAddTransient(typeof(IAppLogger), provider =>
            {
                var factory = provider.GetRequiredService<ILoggerFactory>();
                // Cette factory sera utilisée avec le type générique
                return factory.CreateLogger("Default");
            });

            return services;
        }

        /// <summary>
        /// Ajoute les services de logging avec un logger spécifique pour un type T
        /// </summary>
        /// <typeparam name="T">Type pour lequel le logger est configuré</typeparam>
        /// <param name="services">Collection des services à configurer</param>
        /// <returns>Collection des services configurée avec le logging typé</returns>
        public static IServiceCollection AddAppLogging<T>(this IServiceCollection services) where T : class
        {
            services.TryAddSingleton<ILoggerFactory, LoggerFactory>();
            services.TryAddTransient<IAppLogger>(provider =>
            {
                var factory = provider.GetRequiredService<ILoggerFactory>();
                return factory.CreateLogger<T>();
            });

            return services;
        }
    }
}