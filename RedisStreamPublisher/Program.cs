using System;
using StackExchange.Redis;

namespace RedisStreamPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var redis = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = redis.GetDatabase();

                while (true)
                {
                    Console.WriteLine("Enter message (or 'exit' to quit):");
                    var message = Console.ReadLine();

                    if (message == "exit") break;

                    // We're using "mystream" as the stream name. This can be modified.
                    db.StreamAdd("mystream", "message", message);
                }
            }
        }
    }
}
