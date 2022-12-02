using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SocialNetwork_New.Helper
{
	class Kafka_Helper : IDisposable
	{
		private readonly ProducerConfig _config;
		private readonly IProducer<string, string> _producer;

		public Kafka_Helper(string serverLink)
		{
			_config = new ProducerConfig
			{
				BootstrapServers = serverLink,
				ClientId = Dns.GetHostName(),
				Partitioner = Partitioner.Random
			};

			_producer = new ProducerBuilder<string, string>(_config)
						.SetKeySerializer(Serializers.Utf8)
						.SetValueSerializer(Serializers.Utf8)
						.Build();
		}

		public async Task<bool> InsertPost(string messagejson, string topic)
		{
			try
			{
				DeliveryResult<string, string> val = await _producer.ProduceAsync(
					topic,
					new Message<string, string>
					{
						Value = messagejson
					}
				);

				return true;
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/kafka.txt", ex.ToString() + "\n");
			}

			return false;
		}

		public async Task<bool> InsertPost<T>(IEnumerable<T> listMessage, string topic)
		{
			try
			{
				foreach (T item in listMessage)
				{
					DeliveryResult<string, string> val = await _producer.ProduceAsync(
						topic,
						new Message<string, string>
						{
							Value = String_Helper.ToJson<T>(item)
						}
					);
				}

				_producer.Flush(TimeSpan.FromSeconds(10));

				return true;
			}
			catch (Exception ex)
			{
				File.AppendAllText($"{Environment.CurrentDirectory}/Check/kafka.txt", ex.ToString() + "\n");
			}

			return false;
		}

		public void Flush(TimeSpan ms)
		{
			_producer.Flush(ms);
		}

		public void Dispose()
		{
			if (_producer != null)
			{
				_producer.Dispose();
			}
		}

		~Kafka_Helper()
		{

		}
	}
}
