using Email.Consumer.Model;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(m =>
{
    m.AddConsumer<EmailConsumer>();
    m.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });
        cfg.ConcurrentMessageLimit = 10;
        cfg.ConfigureEndpoints(ctx);
    });
});

var host = builder.Build();
host.Run();
