using System.Net;
using System.Net.Mail;
using DotNetEnv;

namespace CarteiraAtivos.Helpers
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
               string host = Env.GetString("HOST");
               int port = int.Parse(Env.GetString("PORT"));
               string username = Env.GetString("USERNAME");
               string name = "Carteira de Ativos";
               string password = Env.GetString("PASSWORD");

               SmtpClient smtp = new SmtpClient(host, port)
               {
                  Credentials = new NetworkCredential(username, password), 
                  EnableSsl = true
               };

               MailMessage mailMessage = new MailMessage()
               {
                  From = new MailAddress(username, name),
                  Subject = assunto,
                  Body = mensagem,
                  IsBodyHtml = true,
                  Priority = MailPriority.High
               };

               mailMessage.To.Add(email);

               smtp.Send(mailMessage);

               return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar email: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                return false;
            }
        }
    }
}