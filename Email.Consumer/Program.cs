using Email.Consumer.Model;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

var smtpSettings = builder.Configuration.GetSection("SMTP").Get<SmtpSettings>();

// Registrar a instância do SmtpSettings
builder.Services.AddSingleton(smtpSettings);

builder.Services.AddScoped<IEmailService, EmailService>();

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
