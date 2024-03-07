using Application.Interfaces;
using ConsoleSearch;
using ConsoleSearch.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration configuration = ConfigurationHelper.GetConfiguration();

var serviceProvider = new ServiceCollection()
    .AddSingleton<ISearchService, SearchService>()

    .AddTransient<IApp, App>()

    .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<IApp>();

app.Run();
