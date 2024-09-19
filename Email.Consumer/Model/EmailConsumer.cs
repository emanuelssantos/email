using Email.Consumer.Controllers;
using MassTransit;

namespace Email.Consumer.Model;

public class EmailConsumer : IConsumer<Domain.Email>
{
    public async Task Consume(ConsumeContext<Domain.Email> context)
    {
        Console.WriteLine("EXECUTANDO CONSUMER");
        await EmailController.SendEmailAsync(context.Message);

        Console.WriteLine($"SEND EMAIL {DateTime.Now}");
    }
}
