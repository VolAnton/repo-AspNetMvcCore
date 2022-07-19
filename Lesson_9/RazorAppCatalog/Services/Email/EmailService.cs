using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace RazorAppCatalog.Services.Email
{
    public class EmailService : IEmailService
    {
        private const string email = @"затер"; //затер, здесь почта получателя
        private const string subject = @"Новый товар в каталоге";
        private const string serverStatus = @"Статус cервера";
                
        private readonly SmtpConfig _smtpConfig;        
                
        public EmailService(IOptions<SmtpConfig> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            _smtpConfig = options.Value;            
        }

        public async Task SendEmailAsync(Product product, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            MimeMessage? emailMessage = new MimeMessage();
                        
            emailMessage.From.Add(new MailboxAddress("Администрация сайта <Безопасный каталог>", _smtpConfig.UserName));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("Plain")
            {
                Text = $"Добавлен новый товар. Его Id: {product.Id}, название: {product.Name}."
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, false, cancellationToken);
                await client.AuthenticateAsync(_smtpConfig.UserName, _smtpConfig.Password, cancellationToken);
                
                //Ниже строка для проверки работы. Событие перехватывается, сервис не падает.
                //await client.AuthenticateAsync("h", "hhh", cancellationToken);

                await client.SendAsync(emailMessage, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
            }

        }

        public async Task BackgroundSendEmailAsync(string textMessage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            MimeMessage? emailMessage = new MimeMessage();
                        
            emailMessage.From.Add(new MailboxAddress("Администрация сайта <Безопасный каталог>", _smtpConfig.UserName));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = serverStatus;
            emailMessage.Body = new TextPart("Plain")
            {
                Text = textMessage
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, false, cancellationToken);                
                await client.AuthenticateAsync(_smtpConfig.UserName, _smtpConfig.Password, cancellationToken);                                
                await client.SendAsync(emailMessage, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
            }

        }
    }
}
