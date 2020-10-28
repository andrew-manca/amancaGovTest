using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Azure.Identity;
using amancaGovTest;

namespace AlexDickersonAppService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration(config =>
                {
                    var configuration = config.Build();
                    int configCount1 = configuration.AsEnumerable().Count();

                    Environment.SetEnvironmentVariable("AZURE_CLIENT_ID", "0e414e01-14d8-4b01-b43a-123feabd4871");
                    Environment.SetEnvironmentVariable("AZURE_CLIENT_SECRET", ".FR83M1p-Z3gs1XwI4cRFLh4-Li5_511~z");
                    Environment.SetEnvironmentVariable("AZURE_TENANT_ID", "bae50a1b-a7fa-4a2c-b264-944feabfdd7b");
                    var test = new DefaultAzureCredential();


                    config.AddAzureAppConfiguration(options =>
                    {
                    options.Connect(new Uri("https://amancaappconfigkv.azconfig.azure.us"), test).ConfigureKeyVault(kv => kv.SetCredential(test));
                    });

                    //var azureServiceTokenProvider = new AzureServiceTokenProvider();
                    //var keyVaultClient = new KeyVaultClient(
                    //    new KeyVaultClient.AuthenticationCallback(
                    //        azureServiceTokenProvider.KeyVaultTokenCallback));

                    //config.AddAzureKeyVault(
                    //    $"https://{configuration["KeyVaultName"]}.vault.azure.net/",
                    //    keyVaultClient,
                    //    new DefaultKeyVaultSecretManager());

                }
                );
    }
}
