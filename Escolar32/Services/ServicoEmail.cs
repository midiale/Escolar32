using SendGrid;
using SendGrid.Helpers.Mail;

namespace Escolar32.Services
{
    public class ServicoEmail
    {
        public static async Task EnviaEmailAsync(string email, string assunto, string mensagem)
        {
            
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("contato@meutransporteescolar.com.br");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, assunto, mensagem, mensagem);

            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            }
        }
    }
}
