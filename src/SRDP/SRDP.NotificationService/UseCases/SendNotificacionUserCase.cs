using log4net;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.SendNotificacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.NotificationService.UseCases
{
    public class SendNotificacionUserCase : ISendNotificacionUserCase
    {
        private ILog _log;

        public SendNotificacionUserCase(ILog log)
        {
            _log = log;
        }

        public async Task Execute(NotificacionOutput notificacion)
        {
            await Task.Run(() =>
            {
                string hostAddress = ConfigurationManager.AppSettings["Host"].ToString();
                string fromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                bool enableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);

                //creating the object of MailMessage  
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail); //From Email Id  
                mailMessage.Subject = notificacion.Cabecera.Asunto; //Subject of Email  
                mailMessage.Body = notificacion.Mensaje; //body or message of Email  
                mailMessage.IsBodyHtml = true;

                string[] toMuliId = notificacion.Cabecera.Para.Split(',');
                foreach (string toEMailId in toMuliId)
                {
                    if (!String.IsNullOrEmpty(toEMailId))
                        mailMessage.To.Add(new MailAddress(toEMailId)); //adding multiple TO Email Id  
                }

                string[] ccId = notificacion.Cabecera.ConCopia.Split(',');

                foreach (string ccEmail in ccId)
                {
                    if (!String.IsNullOrEmpty(ccEmail))
                        mailMessage.CC.Add(new MailAddress(ccEmail)); //Adding Multiple CC email Id  
                }

                try
                {
                    SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
                    smtp.Host = hostAddress;
                    smtp.EnableSsl = enableSSL;
                    NetworkCredential networkCredentials = new NetworkCredential();
                    networkCredentials.UserName = mailMessage.From.Address;
                    networkCredentials.Password = password;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredentials;
                    smtp.Port = port;
                    smtp.Send(mailMessage); //sending Email 
                    _log.Info("Correo electrónico enviado a " + notificacion.Cabecera.Para);
                }
                catch (Exception ex)
                {
                    _log.Error($"No se ha podido enviar el correo electrónico a {notificacion.Cabecera.Para}. Error='{ex.Message}'");
                    throw;
                }
                 
            });
            
        }
    }
}
