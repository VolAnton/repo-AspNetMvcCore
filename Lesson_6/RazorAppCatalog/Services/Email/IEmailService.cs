namespace RazorAppCatalog.Services.Email
{
    public interface IEmailService
    {
        public Task SendEmailAsync(Product product);

    }
}
