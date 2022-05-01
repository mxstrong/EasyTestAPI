using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using EasyTestAPI.Core.Entities;
using EasyTestAPI.Core.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace EasyTestAPI.Infrastructure;
public class EmailSender : IEmailSender
{
  private readonly EmailConfig _config;
  private readonly ILogger<EmailSender> _logger;

  public EmailSender(ILogger<EmailSender> logger, IOptions<EmailConfig> config)
  {
    _config = config.Value;
    _logger = logger;
  }

  public async Task SendEmailAsync(string to, string subject, string body)
  {
    var email = new MimeMessage();
    email.From.Add(MailboxAddress.Parse(_config.Mail));
    email.To.Add(MailboxAddress.Parse(to));
    email.Subject = "EasyTest";
    email.Body = new TextPart(TextFormat.Html) { Text = body };

    using var smtp = new SmtpClient();
    smtp.ServerCertificateValidationCallback = MySslCertificateValidationCallback;
    await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
    await smtp.AuthenticateAsync(_config.Mail, _config.Password);
    await smtp.SendAsync(email);
    await smtp.DisconnectAsync(true);

    _logger.LogWarning($"Sending email to {to} from {_config.Mail} with subject {subject}.");
  }

  static bool MySslCertificateValidationCallback(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
  {
    return true;
  }

  public async Task SendActivationEmail(User user, ActivationToken token)
  {
    var to = user.Email;
    var subject = "Aktyvuokite savo EasyTest paskyrą";
    var htmlContent = $"<html><body><h2 style=\"font-size: 36\">Sveiki, {user.DisplayName},</h2><br>" +
        "<div style=\"font-size: 20;\">Norėdami užbaigti savo registraciją " +
        $"<a style=\"color: blue\" href=\"https://localhost:57678/api/auth/activate/{token.ActivationTokenId}\">paspauskite čia</a><br><br>" +
        "Jei nekūrėte Politik paskyros, ignoruokite šį laišką.</div></body></html>";
    await SendEmailAsync(to, subject, htmlContent);
  }
}
