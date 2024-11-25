namespace Core
{
    public class AppSettings
    {
        public string[] ClientList { get; set; } = new string[0];
        public string APIUrl { get; set; }
        public string? SecurityKey { get; set; }
        public int TokenExpiryHours { get; set; }

    }
}
