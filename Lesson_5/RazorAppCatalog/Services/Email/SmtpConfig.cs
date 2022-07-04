namespace RazorAppCatalog.Services.Email
#pragma warning disable CS8618 //отключаем null-ворнинги
{
    public class SmtpConfig
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableLogging { get; set; }
    }
}
