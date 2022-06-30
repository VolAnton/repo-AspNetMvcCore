using MailKit.Net.Smtp;
using MimeKit;

namespace RazorAppCatalog.Services.Email
{
    public class EmailService : IEmailService
    {
        private const string email = @"затер"; //затер, здесь почта получателя
        private const string subject = @"Новый товар в каталоге";
        private const string login = @"затер"; //затер, здесь логин
        private const string password = @"затер"; //затер, здесь пароль

        public void SendEmail(Product product)
        {
            MimeMessage? emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта <Безопасный каталог>", login));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("Plain")
            {                
                Text = $"Добавлен новый товар. Его Id: {product.Id}, название: {product.Name}."
            };

            using (var client = new SmtpClient())
            {
                 client.Connect("smtp.beget.com", 25, false);
                 client.Authenticate(login, password);
                 client.Send(emailMessage);                 
            }
            
        }
    }
}
