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

                    //Environment.SetEnvironmentVariable("AZURE_CLIENT_ID", "32b86793-9db5-40bc-84d6-f50a87859afc");
                    //Environment.SetEnvironmentVariable("AZURE_CLIENT_SECRET", "Vro71px9_9VPYXHCI20j6~USP-6vGdyqV-");
                    //Environment.SetEnvironmentVariable("AZURE_TENANT_ID", "72f988bf-86f1-41af-91ab-2d7cd011db47");
                    var test = new DefaultAzureCredential();

                    config.AddAzureAppConfiguration(options =>
                    {
                        options.Connect(new Uri("https://amancaappconfigkv.azconfig.azure.us"), test).ConfigureKeyVault(kv => kv.SetCredential(test));
                    });

                }
                );
    }
}
