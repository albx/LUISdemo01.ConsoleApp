using Microsoft.Extensions.Configuration;
using System.IO;

namespace LUISdemo01.ConsoleApp
{
    public class ConfigurationFactory
    {
        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }
    }
}
