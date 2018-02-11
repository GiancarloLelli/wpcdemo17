using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPC.Listener
{
	class Program
	{
		private static IQueueClient m_client;

		static void Main(string[] args)
		{
			var connectionString = "Endpoint=sb://wpcdynamicsdemo.servicebus.windows.net/;SharedAccessKeyName=Listener-Policy;SharedAccessKey=G2/0TQjFUHF1fL6/qsvHGDNybgm+wpiRcnvUt7XqrF8=";
			var adminConnection = "Endpoint=sb://wpcdynamicsdemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Lu06KzkNZOafpRIj9UKDPtucA77pP+Bv8lafqZTAMSk=";

			var queueName = "pluginqueue";

			m_client = new QueueClient(connectionString, queueName);
			var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler) { MaxConcurrentCalls = 1, AutoComplete = false };
			m_client.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

			var msg = new Message(Encoding.UTF8.GetBytes("Hello World"));
			m_client.SendAsync(msg).GetAwaiter().GetResult();

			Console.ReadLine();
		}

		static Task ProcessMessagesAsync(Message message, CancellationToken token)
		{
			Console.WriteLine($"Received message: {Encoding.UTF8.GetString(message.Body)}");
			return Task.CompletedTask;
		}

		static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
		{
			Console.WriteLine($"Exception {exceptionReceivedEventArgs.Exception}.");
			return Task.CompletedTask;
		}
	}
}
