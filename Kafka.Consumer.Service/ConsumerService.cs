using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Consumer.Service
{
    public class ConsumerService : IHostedService
    {
        IConsumer<string, string> consumer;
        ConsumerConfig config;
        public ConsumerService()
        {
            this.config = new ConsumerConfig
            {
                GroupId = "group1",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                AllowAutoCreateTopics = true,
                EnableAutoCommit = true
            };
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service is started");
            this.consumer = new ConsumerBuilder<string, string>(this.config).Build();
            this.consumer.Subscribe("demo-topic");
            while (true)
            {
                ConsumeResult<string, string> result = consumer.Consume(cancellationToken);
                Console.WriteLine(result.Message.Value);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            consumer?.Dispose();
            return Task.CompletedTask;
        }
    }
}