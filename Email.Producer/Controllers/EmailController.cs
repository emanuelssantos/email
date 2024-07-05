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

            var to = new List<string>() { email };
            _ = _publish.Publish<Domain.Email>(new
            {
                Body = email,
                Subject = "Teste",
                To = to
            });

            return Ok("Email agendado");

        }
        catch (Exception)
        {

            throw;
        }

    }
}
