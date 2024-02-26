using Microsoft.Extensions.Configuration;
using Shared.Model.Settings;
using System.IO;

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

