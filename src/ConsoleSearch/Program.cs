using Application.Interfaces;
using ConsoleSearch;
using ConsoleSearch.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ISearchService, SearchService>(sp => new SearchService(new HttpClient()))

    .AddTransient<IApp, App>()

    .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<IApp>();

app.Run();
