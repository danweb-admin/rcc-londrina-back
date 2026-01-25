using System;
using StackExchange.Redis;

namespace RccManager.Service.Helper
{
	public class RedisHelper
	{
        private static object sync = new object();
        private static ConnectionMultiplexer multiplexer;

        public static StackExchange.Redis.IDatabase GetDatabase()
        {
            return GetMultiplexer().GetDatabase(0);
        }

        public static ConnectionMultiplexer GetMultiplexer()
        {
            var redisHost = Environment.GetEnvironmentVariable("RedisHost");
            var redisPort = Environment.GetEnvironmentVariable("RedisPort");

            try
            {
                if (multiplexer == null)
                {
                    lock (sync)
                    {
                        if (multiplexer == null)
                        {
                            multiplexer = ConnectionMultiplexer
                                .Connect($"{redisHost}:{redisPort},allowAdmin=true");
                        }
                    }
                }

                return multiplexer;
            }
            catch (Exception ex)
            {
                 Console.WriteLine(ex);
                throw;
            }

            
        }
    }
}

