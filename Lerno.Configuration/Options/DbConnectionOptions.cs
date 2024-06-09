namespace Lerno.Configuration.Options
{
    public class DbConnectionOptions : IOptions
    {
        public static string SectionName = "dbConnection";

        public string ConnectionString { get; set; } = string.Empty;

        public string ConnectionTimeout { get; set; } = "00:30:00";
    }
}
