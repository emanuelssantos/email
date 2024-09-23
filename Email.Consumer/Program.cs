using Email.Consumer.Helpers;
using Email.Consumer.Interfaces;
using Email.Consumer.Model;
using Email.Consumer.Services;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

var smtpSettings = builder.Configuration.GetSection("SMTP").Get<SmtpSettings>();

// Registrar a instância do SmtpSettings
builder.Services.AddSingleton(smtpSettings);

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddMassTransit(m =>
{
    m.AddConsumer<EmailConsumer>();
    m.UsingRabbitMq((ctx, cfg) =>
    {
        var configuration = ctx.GetRequiredService<IConfiguration>();
        var host = configuration.GetConnectionString("RabbitMQConnection");
        cfg.Host(host);

        cfg.ConcurrentMessageLimit = 10;
        cfg.ConfigureEndpoints(ctx);
    });
});

var host = builder.Build();
host.Run();
