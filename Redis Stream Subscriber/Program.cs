using System;
using StackExchange.Redis;

namespace RedisStreamSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var redis = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = redis.GetDatabase();

                // Initial reading can be set to start from the oldest message.
                var position = StreamPosition.Beginning;

                while (true)
                {
                    // Reading the stream
                    var messages = db.StreamRead("mystream", position);

                    foreach (var message in messages)
                    {
                        Console.WriteLine($"Received message: {message.Values[0].Value}");
                        position = message.Id; // Setting the position to the last read message ID.
                    }

                    System.Threading.Thread.Sleep(1000); // Pausing for a second before the next read.
                }
            }
        }
    }
}
