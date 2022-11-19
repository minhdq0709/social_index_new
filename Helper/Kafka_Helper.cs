using Confluent.Kafka;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SocialNetwork_New.Helper
{
	class Kafka_Helper
	{
		private static ProducerConfig _config = new ProducerConfig
		{
			BootstrapServers = Config_System.SERVER_LINK,
			ClientId = Dns.GetHostName(),
			Partitioner = Partitioner.Random
		};

		private static IProducer<string, string> producer = new ProducerBuilder<string, string>(_config)
						.SetKeySerializer(Serializers.Utf8)
						.SetValueSerializer(Serializers.Utf8)
						.Build();

		public async Task<bool> InsertPost(string messagejson, string topic)
		{
			try
			{
				DeliveryResult<string, string> val = await producer.ProduceAsync(topic, new Message<string, string> { Value = messagejson });
				producer.Flush(TimeSpan.FromMilliseconds(100));

				return true;
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/kafka.txt", ex.ToString() + "\n");
			}

			return false;
		}
	}
}
