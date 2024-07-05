using MassTransit;

namespace Email.Consumer.Model;

public class EmailConsumer : IConsumer<Domain.Email>
{
    public async Task Consume(ConsumeContext<Domain.Email> context)
    {
        Console.WriteLine($"SEND EMAIL {DateTime.Now}");
    }
}
