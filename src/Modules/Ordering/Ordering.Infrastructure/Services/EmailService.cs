using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Domain.Primitives;
using Ordering.Domain.Services;

namespace Ordering.Infrastructure.Services;

public sealed class EmailService(IOptions<EmailConfiguration> emailConfig, ILogger<EmailService> logger) : IEmailService
{
    private readonly EmailConfiguration _emailConfig = emailConfig.Value;

    public async Task SendEmailAsync(SendEmailDto emailDto, CancellationToken cancellationToken = default)
    {
        // MimeMessage email = new()
        // {
        //     Subject = emailDto.Subject,
        //     To =
        //     {
        //         MailboxAddress.Parse(emailDto.To)
        //     },
        //     Body = new TextPart(TextFormat.Html)
        //     {
        //         Text = emailDto.Html
        //     },
        //     From =
        //     {
        //         MailboxAddress.Parse(_emailConfig.From)
        //     }
        // };
        // using SmtpClient smtp = new();
        // await smtp.ConnectAsync(_emailConfig.Host, _emailConfig.Port, true,
        //     cancellationToken);
        // smtp.AuthenticationMechanisms.Remove("XOAUTH2");
        //
        // await smtp.AuthenticateAsync(_emailConfig.From, _emailConfig.Password, cancellationToken);
        // await smtp.SendAsync(email, cancellationToken);
        // await smtp.DisconnectAsync(true, cancellationToken);
    }
}