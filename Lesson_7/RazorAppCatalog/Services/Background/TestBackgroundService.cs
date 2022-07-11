using RazorAppCatalog.Services.Email;
using System.Diagnostics;

namespace RazorAppCatalog.Services.Background
{
    public class TestBackgroundService : BackgroundService
    {
        private readonly ILogger<TestBackgroundService> _logger;
        private readonly IEmailService _emailService;

        public TestBackgroundService(ILogger<TestBackgroundService> logger, IEmailService emailService)
        {
            ArgumentNullException.ThrowIfNull(logger);
            _logger = logger;

            ArgumentNullException.ThrowIfNull(emailService);
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromHours(1));
            Stopwatch sw = Stopwatch.StartNew();
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                string str = $"Сервер работает уже {sw.Elapsed}";
                _logger.LogInformation(str);
                await _emailService.BackgroundSendEmailAsync(str, stoppingToken);
            }


            throw new NotImplementedException();

        }
    }
}
