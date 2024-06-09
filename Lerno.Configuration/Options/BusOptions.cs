namespace Lerno.Configuration.Options
{
    public class BusOptions : IOptions
    {
        public static string SectionName = "bus";

        public string Host { get; set; } = "localhost";

        public int Port { get; set; } = 5672;

        public string UserName { get; set; } = "guest";

        public string Password { get; set; } = "guest";
    }
}
