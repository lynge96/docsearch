using System.IO;
using ConsoleSearch.Model.Settings;
using Microsoft.Extensions.Configuration;

namespace ConsoleSearch;

class Program
{
    static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        configuration.GetSection("AdvancedSettings").Get<AdvancedSettings>();

        new App().Run();
    }
}

