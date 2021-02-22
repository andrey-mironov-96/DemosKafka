using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
namespace Kafka.Core.Producer
{
   public class KafkaHandle : IDisposable
    {

        private IProducer<byte[], byte[]> producerHandle;

        public Handle Handle { get => this.producerHandle.Handle; }

        public KafkaHandle(string bootstrapServers = "localhost:9092")
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            this.producerHandle = new ProducerBuilder<byte[], byte[]>(config).Build();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
