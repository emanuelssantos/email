﻿using System.Net;
using System.Net.Mail;

namespace Email.Consumer.Controllers;

public class EmailController
{
    private readonly static string EMAIL = "";
    private readonly static string SENHA = "";

    public static async Task<bool> SendEmailAsync(Domain.Email email)
    {
        try
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(EMAIL),
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

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(EMAIL, SENHA);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //smtp.UseDefaultCredentials = true;
            await smtp.SendMailAsync(mail);

            return true;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            throw;
        }

    }
}