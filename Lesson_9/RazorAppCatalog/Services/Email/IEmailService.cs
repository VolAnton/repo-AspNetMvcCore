namespace RazorAppCatalog.Services.Email
{
    public interface IEmailService
    {
        public Task SendEmailAsync(Product product, CancellationToken cancellationToken);

        public Task BackgroundSendEmailAsync(string textMessage, CancellationToken cancellationToken);

    }
}
