using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Confluent.Kafka;
namespace Kafka.Core.Producer
{
    public class ProducerKafka<TKey, TValue>
    {
        private readonly IProducer<TKey, TValue> producer;

        public ProducerKafka(KafkaHandle kafkaHandle)
        {
            this.producer = new DependentProducerBuilder<TKey, TValue>(kafkaHandle.Handle).Build();
        }

        public Task ProducerAsync(string topic, Message<TKey, TValue> message)
            => this.producer.ProduceAsync(topic, message);

        public void Produce(string topic, Message<TKey, TValue> message, Action<DeliveryReport<TKey, TValue>> report)
            => this.producer.Produce(topic, message, report);

        public void Flush(TimeSpan timeout)
        {
            this.producer.Flush(timeout);
        }
    }
}
