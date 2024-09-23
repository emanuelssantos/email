using Email.Consumer.Helpers;
using Email.Consumer.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Email.Consumer.Services;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(SmtpSettings smtpSettings)
    {
        _smtpSettings = smtpSettings;
    }

    public async Task<bool> SendEmailAsync(Domain.Email email)
    {
        try
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_smtpSettings.From),
                Subject = email.Subject,
                Body = email.Body
            };

            // ADD FILES
            if (email.Files != null)
            {
                foreach (var file in email.Files)
                {
                    var stream = new MemoryStream(file.Bytes);
                    var anexo = new Attachment(stream, file.FileName, file.ContentType);
                    mail.Attachments.Add(anexo);
                }
            }

            foreach (var mailTo in email.To)
            {
                mail.To.Add(mailTo);
            }

            await _smtpSettings.SendMailAsync(mail);

            return true;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            throw;
        }
    }
}