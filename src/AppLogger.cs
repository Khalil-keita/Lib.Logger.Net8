using System.Diagnostics;
using System.Runtime.CompilerServices;
using Lib.Logger.Net8.src.Interfaces;
using NLog;

namespace Lib.Logger.Net8.src
{
    public class AppLogger : IAppLogger
    {
        private readonly NLog.Logger _logger;
        private readonly string _categoryName;

        public AppLogger(string categoryName)
        {
            _categoryName = categoryName;
            _logger = LogManager.GetLogger(categoryName);
        }

        public AppLogger(Type type)
        {
            _categoryName = type.FullName ?? string.Empty;
            _logger = LogManager.GetLogger(type.FullName);
        }

        public void LogTrace(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_logger.IsTraceEnabled)
            {
                var eventInfo = CreateLogEventInfo(LogLevel.Trace, message, memberName, sourceFilePath);
                _logger.Log(eventInfo);
            }
        }

        public void LogDebug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_logger.IsDebugEnabled)
            {
                var eventInfo = CreateLogEventInfo(LogLevel.Debug, message, memberName, sourceFilePath);
                _logger.Log(eventInfo);
            }
        }

        public void LogInformation(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_logger.IsInfoEnabled)
            {
                var eventInfo = CreateLogEventInfo(LogLevel.Info, message, memberName, sourceFilePath);
                _logger.Log(eventInfo);
            }
        }

        public void LogWarning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_logger.IsWarnEnabled)
            {
                var eventInfo = CreateLogEventInfo(LogLevel.Warn, message, memberName, sourceFilePath);
                _logger.Log(eventInfo);
            }
        }

        public void LogError(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_logger.IsErrorEnabled)
            {
                var eventInfo = CreateLogEventInfo(LogLevel.Error, message, memberName, sourceFilePath);
                _logger.Log(eventInfo);
            }
        }

        public void LogCritical(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_logger.IsFatalEnabled)
            {
                var eventInfo = CreateLogEventInfo(LogLevel.Fatal, message, memberName, sourceFilePath);
                _logger.Log(eventInfo);
            }
        }

        public void LogError(Exception exception, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_logger.IsErrorEnabled)
            {
                var eventInfo = CreateLogEventInfo(LogLevel.Error, message, memberName, sourceFilePath, exception);
                _logger.Log(eventInfo);
            }
        }

        public void LogCritical(Exception exception, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_logger.IsFatalEnabled)
            {
                var eventInfo = CreateLogEventInfo(LogLevel.Fatal, message, memberName, sourceFilePath, exception);
                _logger.Log(eventInfo);
            }
        }

        public void LogWithContext(string message, object context, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_logger.IsInfoEnabled)
            {
                var eventInfo = CreateLogEventInfo(LogLevel.Info, message, memberName, sourceFilePath);

                if (context != null)
                {
                    eventInfo.Properties["Context"] = System.Text.Json.JsonSerializer.Serialize(context);
                }

                _logger.Log(eventInfo);
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            if (state is IReadOnlyCollection<KeyValuePair<string, object>> properties)
            {
                return ScopeContext.PushProperties(properties);
            }

            // Alternative : convertir l'objet en dictionnaire
            var propertiesDict = state?.GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(state));

            return ScopeContext.PushProperties(propertiesDict);
        }

        public IDisposable BeginTimedOperation(string operationName)
        {
            return new TimedOperation(_logger, operationName);
        }

        public void Flush()
        {
            LogManager.Flush();
        }

        public async Task FlushAsync()
        {
            await Task.Run(() => LogManager.Flush());
        }

        private LogEventInfo CreateLogEventInfo(
            LogLevel level,
            string message,
            string memberName,
            string sourceFilePath,
            Exception? exception = null)
        {
            var eventInfo = new LogEventInfo
            {
                Level = level,
                Message = message,
                LoggerName = _categoryName,
                Exception = exception
            };

            // Ajout des informations de contexte
            eventInfo.Properties["MemberName"] = memberName;
            eventInfo.Properties["SourceFile"] = Path.GetFileName(sourceFilePath);
            eventInfo.Properties["MachineName"] = Environment.MachineName;
            eventInfo.Properties["ProcessId"] = Environment.ProcessId;

            return eventInfo;
        }
    }

    /// <summary>
    /// Classe pour mesurer et logger le temps d'exécution des opérations
    /// Implémente IDisposable pour une utilisation avec le pattern using
    /// </summary>
    internal class TimedOperation : IDisposable
    {
        private readonly NLog.Logger _logger;
        private readonly string _operationName;
        private readonly Stopwatch _stopwatch;
        private bool _disposed;

        /// <summary>
        /// Initialise une nouvelle instance de la classe TimedOperation
        /// </summary>
        /// <param name="logger">Instance du logger NLog pour enregistrer les messages</param>
        /// <param name="operationName">Nom de l'opération à chronométrer</param>
        public TimedOperation(NLog.Logger logger, string operationName)
        {
            _logger = logger;
            _operationName = operationName;
            _stopwatch = Stopwatch.StartNew();

            _logger.Info($"Starting operation: {operationName}");
        }

        /// <summary>
        /// Arrête le chronomètre et logge la durée de l'opération
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _stopwatch.Stop();
                _logger.Info($"Completed operation: {_operationName} in {_stopwatch.ElapsedMilliseconds}ms");
                _disposed = true;
            }
        }
    }
}