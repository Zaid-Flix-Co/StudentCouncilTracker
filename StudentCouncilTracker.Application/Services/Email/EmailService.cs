using System.Net.Mail;
using System.Net;

namespace StudentCouncilTracker.Application.Services.Email;

public interface IEmailService
{
    void SendEmail(string to, string body);
}

public class EmailService : IEmailService
{
    private const string SmtpServer = "smtp.mail.ru";
    private const int Port = 587;
    private const string UserName = "studencheskiy.sovet.kit@mail.ru";
    private const string Password = "jUprRdgq75S5dYasZik4";

    public void SendEmail(string to, string body)
    {
        try
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(UserName);
                message.To.Add(to);
                message.Subject = "Новая задача";
                message.Body = body;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient(SmtpServer, Port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(UserName, Password);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}