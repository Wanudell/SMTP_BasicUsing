using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace SMTP_BasicUsing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IConfiguration config;

        public EmailController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost("send-mail")]
        public IActionResult SendEmail(string body)
        {
            string From = config.GetValue<string>("Email:From");
            string Password = config.GetValue<string>("Email:Password");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(From);
            mail.Subject = "Testing for myself.";
            mail.To.Add(new MailAddress("e-mail address to be sent"));
            mail.Body = $"<html><body> {body} </body></html>";
            mail.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(From, Password),
                EnableSsl = true,
            };

            smtpClient.Send(mail);

            return Ok(mail);
        }
    }
}
