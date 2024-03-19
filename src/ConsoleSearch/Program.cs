using Application.Interfaces;
using ConsoleSearch;
using ConsoleSearch.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Application.Services;
using Loadbalancer;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ISearchService, SearchService>(sp => new SearchService())

    .AddTransient<IApp, App>()

    .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<IApp>();

app.Run();
