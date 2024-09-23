using Email.Domain;
using Microsoft.AspNetCore.Components.Web;
using System.Net;
using System.Net.Mail;

namespace Email.Consumer.Helpers;

public class SmtpSettings
{
    public SmtpSettings(string host, int Port, bool EnableSsl, string UserName, string Password, string From)
    {
        this.Host = host;
        this.Port = Port;
        this.EnableSsl = EnableSsl;
        this.UserName = UserName;
        this.Password = Password;
        this.From = From;

        smtpClient = new SmtpClient(this.Host, this.Port)
        {
            Credentials = new NetworkCredential(this.UserName, this.Password),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = this.EnableSsl
        };

    }

    public string GetFrom()
    {
        return this.From;
    }

    public Task SendMailAsync(MailMessage mail)
    {
        return smtpClient.SendMailAsync(mail);
    }

    private string Host { get; set; }
    private int Port { get; set; }
    private bool EnableSsl { get; set; }
    private string UserName { get; set; }
    private string Password { get; set; }
    public string From { get; set; }

    private SmtpClient smtpClient;
}