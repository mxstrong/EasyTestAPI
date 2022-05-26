using EasyTestAPI.Core.Entities;

namespace EasyTestAPI.Core.Interfaces;
public interface IEmailSender
{
  Task SendEmailAsync(string to, string subject, string body);
  Task SendActivationEmail(User createdUser, ActivationToken token);
}
