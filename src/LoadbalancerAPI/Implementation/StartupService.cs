using LoadbalancerAPI.Interfaces;

public class StartupService : IHostedService, IDisposable
{
    private readonly ILogger<StartupService> _logger;
    private readonly IHealthCheck _healthCheck;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(10); // Adjust the interval as needed
    private Timer _timer;

    public static List<string>? Endpoints;

    public StartupService(ILogger<StartupService> logger, IHealthCheck healthCheck)
    {
        _logger = logger;
        _healthCheck = healthCheck;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application started.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, _interval);

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        Endpoints = _healthCheck.CheckServers().Result;

        _logger.LogInformation($"Retrieved {Endpoints.Count} endpoints on schedule");
        foreach (var endpoint in Endpoints)
        {
            _logger.LogInformation($"Endpoint: {endpoint}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application stopped.");

        // Dispose the timer
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}