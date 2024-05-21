using Core.Settings;
using Microsoft.Extensions.Configuration;

public static class Configuration
{
    public static ConnectionStrings? ConnectionStrings { get; private set; }
    public static AdvancedSettings? AdvancedSettings { get; private set; }
    public static void GetConfiguration()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        AdvancedSettings = configuration.GetSection("AdvancedSettings").Get<AdvancedSettings>();
        ConnectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
    }

}