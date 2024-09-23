using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Consumer.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailAsync(Domain.Email email);
}
