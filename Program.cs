using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
namespace DemoService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) => 
                {
                    var builtconfig = config.Build();
                    var tokenProvider = new AzureServiceTokenProvider();
                    var KeyVaultClient = new KeyVaultClient((authority, resource, scope) => tokenProvider.KeyVaultTokenCallback(authority, resource, scope));
                    config.AddAzureKeyVault(builtconfig["KeyVault:BaseUrl"], KeyVaultClient, new DefaultKeyVaultSecretManager());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
