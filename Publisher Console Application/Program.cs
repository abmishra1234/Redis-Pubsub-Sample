using System;
using StackExchange.Redis;

namespace RedisPublisher
{
    class Program
    {
        private static string message;

        static void Main(string[] args)
        {
            using (var redis = ConnectionMultiplexer.Connect("localhost"))
            {
                var pubsub = redis.GetSubscriber();

                while (true)
                {
                    Console.WriteLine("Enter topic to publish to (or 'exit' to quit):");
                    var topic = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(topic) || topic == "exit") break;

                    Console.WriteLine($"Publishing to topic '{topic}'. Enter message (or 'back' to change topic, 'exit' to quit):");
                    while (true)
                    {
                        var message = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(message) || message == "back" || message == "exit") break;

                        pubsub.Publish(topic, message);
                        Console.WriteLine($"Message '{message}' published to '{topic}'. Enter another message or 'back' to change topic:");
                    }

                    if (message == "exit") break;
                }
            }
        }
    }
}
