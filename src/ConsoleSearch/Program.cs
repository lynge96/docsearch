using Core.Settings;
using Microsoft.Extensions.Configuration;

namespace ConsoleSearch;

class Program
{
    static void Main(string[] args)
    {
        IConfiguration configuration = ConfigurationHelper.GetConfiguration();

        configuration.GetSection("AdvancedSettings").Get<AdvancedSettings>();

        new App().Run();
    }
}

