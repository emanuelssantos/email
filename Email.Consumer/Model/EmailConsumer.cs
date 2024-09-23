using Email.Consumer.Controllers;
using Email.Consumer.Interfaces;
using MassTransit;

namespace Email.Consumer.Model;

public class EmailConsumer : IConsumer<Domain.Email>
{
    private readonly IEmailService _emailService;

    public EmailConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<Domain.Email> context)
    {
        Console.WriteLine("EXECUTANDO CONSUMER");
        await _emailService.SendEmailAsync(context.Message);

        Console.WriteLine($"SEND EMAIL {DateTime.Now}");
    }
}
