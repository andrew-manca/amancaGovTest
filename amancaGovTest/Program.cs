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


        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //   WebHost.CreateDefaultBuilder(args)
        //          .ConfigureAppConfiguration((hostingContext, config) =>
        //          {
        //              var settings = config.Build();
        //              config.AddAzureAppConfiguration(options =>
        //                  options.Connect(new Uri(settings["AppConfig:Endpoint"]), new ManagedIdentityCredential()));
        //          })
        //          .UseStartup<Startup>();
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration(config =>
                {
                    var configuration = config.Build();
                    int configCount1 = configuration.AsEnumerable().Count();

                    //Environment.SetEnvironmentVariable("AZURE_CLIENT_ID", "f28f6299-23bd-47e7-9045-ce9019264461");
                    //Environment.SetEnvironmentVariable("AZURE_CLIENT_SECRET", "Tezv1pt1~Dtp09z~6~Hp.~MH3PCyYMniR~");
                    //Environment.SetEnvironmentVariable("AZURE_TENANT_ID", "72f988bf-86f1-41af-91ab-2d7cd011db47");
                    var credOptions = new DefaultAzureCredentialOptions { AuthorityHost = AzureAuthorityHosts.AzureGovernment };
                    var test = new  DefaultAzureCredential(credOptions);



                    config.AddAzureAppConfiguration(options =>
                    {
                        options.Connect("Endpoint=https://amancaappconfigkv.azconfig.azure.us;Id=62OG-laf-s0:aGYGQOjibaiudCRXx17v;Secret=rf2+GRmezIN74SzikOR6SEUzkgIoBJnJRWvTLk53r6o=").ConfigureKeyVault(kv => kv.SetCredential(new ManagedIdentityCredential()));
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
