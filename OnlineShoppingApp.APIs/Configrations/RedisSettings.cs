namespace OnlineShoppingApp.APIs.Configrations
{
    public class RedisSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public int ExpirationInSeconds { get; set; }
        public string BaseCurrency { get; set; } = string.Empty;
    }
}
