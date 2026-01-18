using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public interface IEmailSender
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}

public class EmailSender : IEmailSender
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public EmailSender(IConfiguration configuration)
    {
        _smtpHost = configuration["Email:SmtpHost"] 
            ?? throw new ArgumentNullException("Email:SmtpHost is required");
        _smtpPort = int.Parse(configuration["Email:SmtpPort"] 
            ?? throw new ArgumentNullException("Email:SmtpPort is required"));
        _smtpUser = configuration["Email:SmtpUser"] 
            ?? throw new ArgumentNullException("Email:SmtpUser is required");
        _smtpPass = configuration["Email:SmtpPass"] 
            ?? throw new ArgumentNullException("Email:SmtpPass is required");
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MailMessage();
        message.From = new MailAddress(_smtpUser);
        message.To.Add(toEmail);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = false;

        using var client = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }
}
