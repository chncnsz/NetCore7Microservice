using StackExchange.Redis;

namespace FreeCourse.Services.BasketAPI.Services
{
    public class RedisService
    {
        public readonly string _host;
        public readonly int _port;
        private ConnectionMultiplexer _ConnectionMultiplexer;
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDb(int db = 1) => _ConnectionMultiplexer.GetDatabase(db);
    }
}
