using Ordering.Domain.Primitives;

namespace Ordering.Domain.Services;

public interface IEmailService
{
    Task SendEmailAsync(SendEmailDto emailDto, CancellationToken cancellationToken = default);
}