using SRDP.Application.Repositories;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.GetNotificacion;
using SRDP.Application.UseCases.GetNotificationQ;
using SRDP.Application.UseCases.GetProfile;
using SRDP.Application.UseCases.SendNotificacion;
using SRDP.Application.UseCases.UpdateNotificationQ;
using SRDP.Domain.Declaraciones;
using SRDP.Domain.Enumerations;
using SRDP.Domain.Notificaciones;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.ProcessNotificationQueue
{
    public class ProcessNotificationQueueUserCase : IProcessNotificationQueueUserCase
    {
        private readonly IGetNotificationQUserCase _getNotificationQUserCase;
        private readonly IUpdateNotificationQUserCase _updateNotificationQUserCase;
        private readonly ISendNotificacionUserCase  _sendNotificacionUserCase;
        private readonly IFuncionarioUsuarioReadOnlyRepository _funcionarioUsuarioReadOnlyRepository;
        private readonly IDeclaracionWriteOnlyRepository _declaracionWriteOnlyRepository;
        private readonly IGetGestionesUserCase _getGestionesUserCase;

        public ProcessNotificationQueueUserCase(IGetNotificationQUserCase getNotificationQUserCase, IUpdateNotificationQUserCase updateNotificationQUserCase, 
            ISendNotificacionUserCase sendNotificacionUserCase, IFuncionarioUsuarioReadOnlyRepository funcionarioUsuarioReadOnlyRepository,
            IDeclaracionWriteOnlyRepository declaracionWriteOnlyRepository, IGetGestionesUserCase getGestionesUserCase)
        {
            _getNotificationQUserCase = getNotificationQUserCase;
            _updateNotificationQUserCase = updateNotificationQUserCase;
            _sendNotificacionUserCase = sendNotificacionUserCase;
            _funcionarioUsuarioReadOnlyRepository = funcionarioUsuarioReadOnlyRepository;
            _declaracionWriteOnlyRepository = declaracionWriteOnlyRepository;
            _getGestionesUserCase = getGestionesUserCase;
        }

        public async Task<bool> Execute(string webServerURL, string fromAddress)
        {
            var queueItems = await _getNotificationQUserCase.ExecuteList(QueueStatus.Queued);
            var result = false;
            foreach (var item in queueItems)
            {
                try
                {
                    var userProfile = await _funcionarioUsuarioReadOnlyRepository.Get(item.UserName);
                    if (userProfile == null)
                        throw new ApplicationException($"El usuario {item.UserName} no tiene un Funcionario asociado");

                    var cabeceraNotificacion = new CabeceraEmailOutput(fromAddress, userProfile.Email, String.Empty, "SRDP - Se ha creado un nuevo formulario de Declaración Patrimonial para su usuario", DateTime.Now);
                    var gestionVigente = await _getGestionesUserCase.GestionVigente();
                    var declaracion = new Declaracion(item.FuncionarioID,
                        new Gestion(gestionVigente.Anio, gestionVigente.FechaInicio, gestionVigente.FechaFinal, gestionVigente.Vigente), DateTime.Now, EstadoDeclaracion.Nueva);
                    await _declaracionWriteOnlyRepository.Add(declaracion);

                    string mensaje = CreateMensajeBody(webServerURL, declaracion);
                    var notificacion = new Notificacion(item.FuncionarioID, TipoNotificacion.Email, cabeceraNotificacion.JsonSerialize(), mensaje, false, false);

                    await _sendNotificacionUserCase.Execute(new NotificacionOutput(notificacion.ID, notificacion.FuncionarioID, notificacion.TipoNotificacion.ToString(),
                        notificacion.Cabecera, notificacion.Mensaje, notificacion.Procesado, notificacion.Leido, notificacion.FechaCreacion, notificacion.FechaModificacion));
                    var notificacionProcesada = await _updateNotificationQUserCase.Execute(new NotificacionQueueItem(item.FuncionarioID, item.UserName, ActionType.Alta, QueueStatus.Processed));
                    var queueItem = NotificacionQueueItem.Load(item.ID, item.FuncionarioID, item.UserName, ActionType.Alta, QueueStatus.Processed, item.CreateDate, DateTime.Now, String.Empty);
                    await _updateNotificationQUserCase.Execute(queueItem);
                    result = result && true;
                }
                catch (Exception ex)
                {
                    var queueItem = NotificacionQueueItem.Load(item.ID, item.FuncionarioID, item.UserName, ActionType.Alta, QueueStatus.Error, item.CreateDate, DateTime.Now, ex.Message);
                    await _updateNotificationQUserCase.Execute(queueItem);
                    throw;
                }
            }
            return result;
        }

        private string CreateMensajeBody(string webServerURL, Declaracion declaracion)
        {
            if (webServerURL.Last() != '/')
                webServerURL += "/";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.AppendLine("<html xmlns = \"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<title> SRDP </title>");
            sb.AppendLine("<style type = \"text/css\">");
            sb.AppendLine("body");
            sb.AppendLine("{");
            sb.AppendLine("background: #fff;");
            sb.AppendLine("font-size: .80em;");
            sb.AppendLine("font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif;");
            sb.AppendLine("margin: 0px;");
            sb.AppendLine("padding: 0px;");
            sb.AppendLine("color: #696969;");
            sb.AppendLine("}");
            sb.AppendLine(".logo");
            sb.AppendLine("{");
            sb.AppendLine("float:left;");
            sb.AppendLine("padding: 4px 4px 4px 8px;");
            sb.AppendLine("z-index:1;");
            sb.AppendLine("}");
            sb.AppendLine(".bigger");
            sb.AppendLine("{");
            sb.AppendLine("font-size:1.2em;");
            sb.AppendLine("}");
            sb.AppendLine("h1");
            sb.AppendLine("{");
            sb.AppendLine("font-size: 1.6em;");
            sb.AppendLine("padding-bottom: 0px;");
            sb.AppendLine("margin-bottom: 0px;");
            sb.AppendLine("}");
            sb.AppendLine(".title");
            sb.AppendLine("{");
            sb.AppendLine("display: block;");
            sb.AppendLine("float: left;");
            sb.AppendLine("text-align: left;");
            sb.AppendLine("width: auto;");
            sb.AppendLine("}");
            sb.AppendLine(".header");
            sb.AppendLine("{");
            sb.AppendLine("position: relative;");
            sb.AppendLine("margin: 0px;");
            sb.AppendLine("padding: 0px;");
            sb.AppendLine("background: #4b6c9e;");
            sb.AppendLine("width: 100%;");
            sb.AppendLine("}");
            sb.AppendLine(".header h1");
            sb.AppendLine("{");
            sb.AppendLine("font-weight: 300;");
            sb.AppendLine("margin: 0px;");
            sb.AppendLine("padding: 0px 0px 0px 20px;");
            sb.AppendLine("color: #f9f9f9;");
            sb.AppendLine("border: none;");
            sb.AppendLine("line-height: 2em;");
            sb.AppendLine("font-size: 1.8em;");
            sb.AppendLine("}");
            sb.AppendLine(".main");
            sb.AppendLine("{");
            sb.AppendLine("padding: 0px 0px 8px 0px;");
            sb.AppendLine("margin: 0px;");
            sb.AppendLine("min-height: 420px;");
            sb.AppendLine("}");
            sb.AppendLine("a: link, a: visited, a: active");
            sb.AppendLine("{");
            sb.AppendLine("color: #336699;");
            sb.AppendLine("text-decoration:none;");
            sb.AppendLine("}");
            sb.AppendLine("a: hover");
            sb.AppendLine("{");
            sb.AppendLine("color: #6BCB01;");
            sb.AppendLine("}");
            sb.AppendLine("p");
            sb.AppendLine("{");
            sb.AppendLine("margin-bottom: 10px;");
            sb.AppendLine("line-height: 1.2em;");
            sb.AppendLine("}");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");

            sb.AppendLine("<body>");
            sb.AppendLine("<div style = \"width: 600px; height: 300px; margin: 5px auto 0px auto; border: 1px solid #496077;background-color: #fff;\">");
            sb.AppendLine("<div class=\"header\">");
            sb.AppendLine("<span></span><br/>");
            sb.AppendLine("<div>");
            sb.AppendLine("<h1>");
            sb.AppendLine("<span class=\"bigger\">S</span>ISTEMA DE <span class=\"bigger\">R</span>EGISTRO DE <span class=\"bigger\">D</span>ECLARACIONES <span class=\"bigger\">P</span>ATRIMONIALES");
            sb.AppendLine("</h1>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"main\">");
            sb.AppendLine("<table cellpadding = \"2\" cellspacing=\"2\" border=\"0\" width=\"100%\">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");
            sb.AppendLine("<p>ESTE CORREO ELECTRONICO REQUIERE SU ATENCIÓN PERSONAL</p>");
            sb.AppendLine("<p>El sistema SRDP ha creado un formulario de Declaración Patrimonial que debe completar</p>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");
            sb.AppendLine("<p>Para acceder al formulario presione en el vínculo Ingresar</p>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");
            sb.AppendLine($"<a href = '{webServerURL}declaraciones/details/{declaracion.ID}'> Ingresar </a>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");


            return sb.ToString();
        }
    }
}
