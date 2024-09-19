using Email.Domain;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Email.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private IPublishEndpoint _publish;
    public EmailController(IPublishEndpoint publishEndpoint)
    {
        _publish = publishEndpoint;
    }

    [HttpPost]
    [Route("SendEmail")]
    public IActionResult SendEmail(string email, IFormFile[] files)
    {
        try
        {
            var emailFile = new List<EmailFile>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        emailFile.Add(new EmailFile
                        {
                            ContentType = file.ContentType,
                            FileName = file.FileName,
                            Bytes = fileBytes
                        });
                    }
                }
            }


            var to = new List<string>() { email };
            _ = _publish.Publish<Domain.Email>(new
            {
                Body = email,
                Subject = "Teste",
                To = to,
                Files = emailFile
            });

            return Ok("Email agendado");

        }
        catch (Exception)
        {

            throw;
        }

    }
}
