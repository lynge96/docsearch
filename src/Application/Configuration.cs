using Core.Settings;
using Microsoft.Extensions.Configuration;

public static class Configuration
{
    public static void GetConfiguration()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        configuration.GetSection("AdvancedSettings").Get<AdvancedSettings>();
        configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
    }

}