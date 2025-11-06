using Lib.Logger.Net8.src.Interfaces;

namespace Lib.Logger.Net8.src.Factory
{
    /// <summary>
    /// Interface définissant une fabrique pour créer des instances de IAppLogger
    /// </summary>
    internal interface ILoggerFactory
    {
        /// <summary>
        /// Crée un logger avec un nom de catégorie spécifique
        /// </summary>
        /// <param name="categoryName">Nom de la catégorie du logger</param>
        /// <returns>Instance de IAppLogger configurée pour la catégorie spécifiée</returns>
        IAppLogger CreateLogger(string categoryName);

        /// <summary>
        /// Crée un logger avec le type spécifié comme catégorie
        /// </summary>
        /// <typeparam name="T">Type utilisé pour déterminer la catégorie du logger</typeparam>
        /// <returns>Instance de IAppLogger configurée pour le type spécifié</returns>
        IAppLogger CreateLogger<T>();
    }

    /// <summary>
    /// Implémentation concrète de ILoggerFactory pour créer des instances de AppLogger
    /// </summary>
    internal class LoggerFactory : ILoggerFactory
    {
        public IAppLogger CreateLogger(string categoryName)
        {
            return new AppLogger(categoryName);
        }

        public IAppLogger CreateLogger<T>()
        {
            return new AppLogger(typeof(T));
        }
    }
}