using Application.Interfaces;
using Application.Services;
using ConsoleSearch;
using ConsoleSearch.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ISearchService, SearchService>(sp => new SearchService())

    .AddTransient<IApp, App>()

    .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<IApp>();

app.Run();
