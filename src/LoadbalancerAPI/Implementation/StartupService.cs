using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LoadbalancerAPI.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Denne klasse implementerer IHostedService-interfacet og fungerer som en opstarts-tjeneste.
// StartupService er ansvarlig for at starte og stoppe applikationen samt udføre sundhedstjek på serverne med jævne mellemrum.

public class StartupService : IHostedService, IDisposable
{
    private readonly ILogger<StartupService> _logger;
    private readonly IHealthCheck _healthCheck;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(10); // Adjust the interval as needed
    private Timer _timer;

    // En statisk variabel til at gemme tilgængelige endepunkter
    public static Dictionary<string, string>? Endpoints;

    // Konstruktør til at initialisere logger og IHealthCheck
    public StartupService(ILogger<StartupService> logger, IHealthCheck healthCheck)
    {
        _logger = logger;
        _healthCheck = healthCheck;
    }

    // Metode til at starte opstartstjenesten
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application started.");

        // Opretter en timer til at udføre sundhedstjek med det angivne interval
        _timer = new Timer(DoWork, null, TimeSpan.Zero, _interval);

        return Task.CompletedTask;
    }

    // Metode, der udfører sundhedstjek på servere og opdaterer Endpoints-dictionary
    //Denne metode udfører selve arbejdet ved at kalde CheckServers-metoden på _healthCheck og opdatere Endpoints-dictionaryen
    //med de returnerede resultater.
    private void DoWork(object state)
    {
        Endpoints = _healthCheck.CheckServers().Result;

        // Logger antallet af tilgængelige endepunkter og hver af dem
        _logger.LogInformation($"Retrieved {Endpoints.Count} endpoints on schedule");
        foreach (var endpoint in Endpoints)
        {
            _logger.LogInformation($"Endpoint: {endpoint}");
        }
    }

    // Metode til at stoppe opstartstjenesten
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application stopped.");

        // Stopper timeren og frigiver ressourcer
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    // Metode til at frigive ressourcer
    public void Dispose()
    {
        _timer?.Dispose();
    }
}