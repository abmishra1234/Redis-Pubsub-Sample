using System;
using StackExchange.Redis;

namespace RedisSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var redis = ConnectionMultiplexer.Connect("localhost"))
            {
                var pubsub = redis.GetSubscriber();

                Console.WriteLine("Enter topics you want to subscribe to (comma-separated, e.g., 'topic1,topic2'):");
                var topicsInput = Console.ReadLine();
                var topics = topicsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var topic in topics)
                {
                    pubsub.Subscribe(topic.Trim(), (channel, message) =>
                    {
                        Console.WriteLine($"Received message on {channel}: {message}");
                    });

                    Console.WriteLine($"Subscribed to {topic.Trim()}");
                }

                Console.WriteLine("Listening for messages. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}