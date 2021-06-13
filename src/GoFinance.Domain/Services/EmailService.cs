using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Services;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GoFinance.Domain.Services
{
    public class EmailService : IEmailService
    {
        public async Task Enviar(Smtp smtp, string email, string title, string body)
        {
            SmtpClient client = new SmtpClient();
            client.Host = smtp.Host;
            client.EnableSsl = smtp.SSL;
            client.Credentials = new System.Net.NetworkCredential(smtp.Usuario, smtp.Senha);

            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress(smtp.Email.Endereco, smtp.Mascara);
            mail.From = new MailAddress(smtp.Email.Endereco, smtp.Mascara);
            mail.To.Add(new MailAddress(email));
            mail.Subject = title;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                await client.SendMailAsync(mail);
            }
            catch
            {
                //trata erro
            }
            finally
            {
                mail = null;
            }
        }
    }
}
