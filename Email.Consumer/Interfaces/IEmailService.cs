namespace Email.Consumer.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailAsync(Domain.Email email);
}
