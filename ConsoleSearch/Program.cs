using System.IO;
using ConsoleSearch.Model;
using Microsoft.Extensions.Configuration;

namespace ConsoleSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var appSettings = configuration.GetSection("AdvancedSettings").Get<AppSettings>();

            new App().Run(appSettings);
        }
    }
}
