using AWS_ntegratorMySQL.Model;
using AWSIntegratorMySQL.Domain.Entities;
using AWSIntegratorMySQL.Domain.Interfaces.Repositories;
using AWSIntegratorMySQL.Domain.Interfaces.Services;
using AWSIntegratorMySQL.Domain.Services;
using AWSIntegratorMySQL.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AWS_ntegratorMySQL
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var awsService = serviceProvider.GetService<IAwsService>();
            var updaterDatabase = serviceProvider.GetService<IUpdaterDatabase>();

            do
            {
                try
                {
                    var responseAws = await awsService.GetMessageAsync();

                    if (responseAws.Messages.Count > 0)
                    {
                        var awsReceiveMessage = JsonSerializer.Deserialize<AwsReceiveMessage>(responseAws.Messages[0].Body);
                        if (awsReceiveMessage.Records != null)
                        {
                            var objectS3 = awsReceiveMessage.Records[0].S3.ObjectS3;
                            Console.WriteLine($"Processando o arquivo: '{objectS3.Key}'");
                            var fileEntity = new FileEntity(objectS3.Key, objectS3.Size, awsReceiveMessage.Records[0].EventTime);

                            updaterDatabase.Update(fileEntity);
                            await awsService.DeleteMessage(responseAws.Messages[0]);
                            Console.WriteLine($"Arquivo: '{objectS3.Key}' processado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro no processamento dos dados. Erro: {ex.Message}");
                    throw;
                }
            } while (!Console.KeyAvailable);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddScoped<IConfiguration>(_ => configuration);

            services.AddScoped<IAwsService, AwsService>();
            services.AddScoped<IUpdaterDatabase, UpdaterDatabase>();

            services.AddScoped<IFileRepository, FileRepository>();
        }
    }
}
