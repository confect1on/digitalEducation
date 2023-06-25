using Microsoft.AspNetCore.Identity.UI.Services;

namespace DigitalEducation.Services;

public class FakeEmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        throw new NotImplementedException();
    }
}