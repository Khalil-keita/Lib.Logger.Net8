# Lib.Logger.Net8

Une bibliothèque de logging complète et flexible pour .NET 8, basée sur NLog avec une interface simplifiée et des fonctionnalités avancées.

## Fonctionnalités

- ✅ Interface simple et intuitive (`IAppLogger`)
- ✅ Support de tous les niveaux de log (Trace, Debug, Information, Warning, Error, Critical)
- ✅ Logging structuré en JSON
- ✅ Scopes de logging pour le contexte
- ✅ Opérations chronométrées
- ✅ Configuration flexible via NLog
- ✅ Logging asynchrone
- ✅ Archivage automatique
- ✅ Sortie console colorée

### Demarrage

## (App console)
```C#
using Lib.Logger.Net8.src.Config;
using Lib.Logger.Net8.src.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddAppLogging<Program>();

using var host = builder.Build();
var logger = host.Services.GetRequiredService<IAppLogger>();

logger.LogInformation("Application démarrée !");
```

## (App Web)
```C#
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppLogging<Program>();

var app = builder.Build();
// Votre code d'application
```

## logging basique
```C#
public class MonService
{
    private readonly IAppLogger _logger;

    public MonService(IAppLogger logger)
    {
        _logger = logger;
    }

    public void TraiterDonnees()
    {
        _logger.LogInformation("Traitement démarré");
        _logger.LogDebug("Information de débogage détaillée");
        _logger.LogError("Une erreur est survenue");
    }
}
```

## logging avec scope
```C#
using (_logger.BeginScope("ContexteOperation"))
{
    _logger.LogInformation("Dans le scope");
    // Votre code
}
```

## Opérations Chronométrées
```C#
using (_logger.BeginTimedOperation("CalculComplexe"))
{
    // Votre opération longue
    // Le temps d'exécution sera loggé automatiquement
}
```

## Logging avec Contexte
```C#
var context = new { UserId = 123, Action = "Update" };
_logger.LogWithContext("Action utilisateur", context)
```
