using EasyTestAPI.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace EasyTestAPI.Infrastructure;
public class EmailSender : IEmailSender
{
  private readonly ILogger<EmailSender> _logger;

  public EmailSender(ILogger<EmailSender> logger)
  {
    _logger = logger;
  }

  public async Task SendEmailAsync(string to, string from, string subject, string body)
  {
    var emailClient = new SmtpClient("localhost");
    var message = new MailMessage
    {

      From = new MailAddress(from),
      Subject = subject,
      Body = body


<<<<<<< Updated upstream
    };
    message.To.Add(new MailAddress(to));
    await emailClient.SendMailAsync(message);
    _logger.LogWarning($"Sending email to {to} from {from} with subject {subject}.");
=======
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
        $"<a style=\"color: blue\" href=\"http://localhost:57678/api/auth/activate/{token.ActivationTokenId}\">paspauskite čia</a><br><br>" +
        "Jei nekūrėte Politik paskyros, ignoruokite šį laišką.</div></body></html>";
    await SendEmailAsync(to, subject, htmlContent);
>>>>>>> Stashed changes
  }
}
