using System.Runtime.CompilerServices;

namespace Lib.Logger.Net8.src.Interfaces
{
    /// <summary>
    /// Interface définissant les opérations de logging pour l'application
    /// </summary>
    public interface IAppLogger
    {
        /// <summary>
        /// Log un message avec le niveau de trace
        /// </summary>
        /// <param name="message">Message à logger</param>
        /// <param name="memberName">Nom de la méthode appelante (rempli automatiquement)</param>
        /// <param name="sourceFilePath">Chemin du fichier source (rempli automatiquement)</param>
        void LogTrace(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log un message avec le niveau de debug
        /// </summary>
        /// <param name="message">Message à logger</param>
        /// <param name="memberName">Nom de la méthode appelante (rempli automatiquement)</param>
        /// <param name="sourceFilePath">Chemin du fichier source (rempli automatiquement)</param>
        void LogDebug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log un message avec le niveau d'information
        /// </summary>
        /// <param name="message">Message à logger</param>
        /// <param name="memberName">Nom de la méthode appelante (rempli automatiquement)</param>
        /// <param name="sourceFilePath">Chemin du fichier source (rempli automatiquement)</param>
        void LogInformation(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log un message avec le niveau d'avertissement
        /// </summary>
        /// <param name="message">Message à logger</param>
        /// <param name="memberName">Nom de la méthode appelante (rempli automatiquement)</param>
        /// <param name="sourceFilePath">Chemin du fichier source (rempli automatiquement)</param>
        void LogWarning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log un message avec le niveau d'erreur
        /// </summary>
        /// <param name="message">Message à logger</param>
        /// <param name="memberName">Nom de la méthode appelante (rempli automatiquement)</param>
        /// <param name="sourceFilePath">Chemin du fichier source (rempli automatiquement)</param>
        void LogError(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log un message avec le niveau critique
        /// </summary>
        /// <param name="message">Message à logger</param>
        /// <param name="memberName">Nom de la méthode appelante (rempli automatiquement)</param>
        /// <param name="sourceFilePath">Chemin du fichier source (rempli automatiquement)</param>
        void LogCritical(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log une exception avec le niveau d'erreur
        /// </summary>
        /// <param name="exception">Exception à logger</param>
        /// <param name="message">Message à logger</param>
        /// <param name="memberName">Nom de la méthode appelante (rempli automatiquement)</param>
        /// <param name="sourceFilePath">Chemin du fichier source (rempli automatiquement)</param>
        void LogError(Exception exception, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log une exception avec le niveau critique
        /// </summary>
        /// <param name="exception">Exception à logger</param>
        /// <param name="message">Message à logger</param>
        /// <param name="memberName">Nom de la méthode appelante (rempli automatiquement)</param>
        /// <param name="sourceFilePath">Chemin du fichier source (rempli automatiquement)</param>
        void LogCritical(Exception exception, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log un message avec un contexte supplémentaire
        /// </summary>
        /// <param name="message">Message à logger</param>
        /// <param name="context">Objet de contexte contenant des informations supplémentaires</param>
        /// <param name="memberName">Nom de la méthode appelante (rempli automatiquement)</param>
        /// <param name="sourceFilePath">Chemin du fichier source (rempli automatiquement)</param>
        void LogWithContext(string message, object context, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Débute un scope de logging avec un état spécifique
        /// </summary>
        /// <typeparam name="TState">Type de l'état du scope</typeparam>
        /// <param name="state">État du scope</param>
        /// <returns>Disposable qui fermera le scope lorsqu'il sera disposé</returns>
        IDisposable BeginScope<TState>(TState state);

        /// <summary>
        /// Débute une opération chronométrée
        /// </summary>
        /// <param name="operationName">Nom de l'opération à mesurer</param>
        /// <returns>Disposable qui enregistrera la durée lorsqu'il sera disposé</returns>
        IDisposable BeginTimedOperation(string operationName);

        /// <summary>
        /// Vide les buffers de logging et assure que tous les messages en attente sont écrits
        /// </summary>
        void Flush();

        /// <summary>
        /// Vide les buffers de logging de manière asynchrone et assure que tous les messages en attente sont écrits
        /// </summary>
        /// <returns>Tâche représentant l'opération de flush</returns>
        Task FlushAsync();
    }
}