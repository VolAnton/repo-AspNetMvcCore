using Polly;
using RazorAppCatalog.Services.Email;
using System.Threading;

namespace RazorAppCatalog.DomainEvents.Handlers
{
    public class ProductAddedEmailSender : BackgroundService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ProductAddedEmailSender> _logger;

        public ProductAddedEmailSender(IEmailService emailService, ILogger<ProductAddedEmailSender> logger)
        {
            ArgumentNullException.ThrowIfNull(emailService);
            _emailService = emailService;

            ArgumentNullException.ThrowIfNull(logger);
            _logger = logger;

            DomainEventsManager.Register<ProductAdded>((e) => _ = SendEmailNotification(e));
            DomainEventsManager.Register<ProductAdded>((ev) => _logger.LogInformation("Продукт успешно добавлен"));
        }

        public async Task SendEmailNotification(ProductAdded productAdded)
        {
            try
            {
                await _emailService.SendEmailAsync(productAdded.Product, productAdded.CancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка при отправке email");
            }

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

    }
}
