using Core.Settings;
using Microsoft.Extensions.Configuration;

public class ConfigurationHelper
{
    public static IConfiguration GetConfiguration()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        configuration.GetSection("AdvancedSettings").Get<AdvancedSettings>();
        configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

        return configuration;
    }
}