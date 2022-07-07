using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace RazorAppCatalog.Services.Email
{
    public class EmailService : IEmailService
    {
        private const string email = @"затер"; //затер, здесь почта получателя
        private const string subject = @"Новый товар в каталоге";

        private readonly IConfiguration _configuration;
        private readonly SmtpConfig _smtpConfig;

        private string _userName;
        private string _userPassword;

        public EmailService(IOptions<SmtpConfig> options, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(options);
            _smtpConfig = options.Value;
            _configuration = configuration;

            ArgumentNullException.ThrowIfNull(configuration);
            _userName = _configuration["SmtpConfig:UserName"];
            _userPassword = _configuration["SmtpConfig:UserPassword"];
        }

        public async Task SendEmailAsync(Product product)
        {
            MimeMessage? emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта <Безопасный каталог>", _userName));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("Plain")
            {
                Text = $"Добавлен новый товар. Его Id: {product.Id}, название: {product.Name}."
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, false);
                await client.AuthenticateAsync(_userName, _userPassword);
                await client.SendAsync(emailMessage);
            }

        }
    }
}
