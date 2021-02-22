using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
namespace Kafka.Consumer.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, colletions) =>
                {
                    colletions.AddHostedService<ConsumerService>();
                });

    }
}
