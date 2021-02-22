using Confluent.Kafka;
using Kafka.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Producer.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly ProducerConfig config;
        private readonly string[] names;
        public ProducerController(ProducerConfig config)
        {
            this.config = config;
            this.names = new[] { "Иван", "Анастасия", "Андрей", "Дмитрий", "Ольга", "Татьяна", "Михаил", "Ян" };
        }

        [HttpGet("CreateEmployee")]
        public async Task<ActionResult<string>> CreateEmployee()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string nameTopic = "demo-topic";
            using (var producer = new ProducerBuilder<string, string>(this.config).Build())
            {
                while (true)
                {
                    await producer.ProduceAsync(nameTopic, new Message<string, string>
                    {
                        Key = Guid.NewGuid().ToString(),
                        Value = GenerateEmployee()
                    });
                    producer.Flush(TimeSpan.FromMilliseconds(5));
                    Thread.Sleep(1000);
                }
            }

            return Ok();
        }

        private string GenerateEmployee()
        {
            Random rnd = new Random();
            Employee employee = new Employee
            {
                Name = this.names[rnd.Next(0, 7)],
                Age = rnd.Next(18, 65)
            };
            return JsonConvert.SerializeObject(employee);
        }
    }
}
