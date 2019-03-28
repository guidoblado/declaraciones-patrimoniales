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

        public async Task<bool> Execute(string serverPath, string fromAddress)
        {
            var queueItems = await _getNotificationQUserCase.ExecuteList(QueueStatus.Queued);
            var result = false;
            foreach (var item in queueItems)
            {
                var userProfile = await _funcionarioUsuarioReadOnlyRepository.Get(item.UserName);
                var cabeceraNotificacion = new CabeceraEmailOutput(fromAddress, userProfile.Email, String.Empty, "SRDP - Se ha creado un nuevo formulario de Declaración Patrimonial para su usuario", DateTime.Now);
                var gestionVigente = await _getGestionesUserCase.GestionVigente();
                var declaracion = new Declaracion(item.FuncionarioID,
                    new Gestion(gestionVigente.Anio, gestionVigente.FechaInicio, gestionVigente.FechaFinal, gestionVigente.Vigente), DateTime.Now, EstadoDeclaracion.Nueva);
                await _declaracionWriteOnlyRepository.Add(declaracion);

                string mensaje = CreateMensajeBody(declaracion);
                var notificacion = new Notificacion(item.FuncionarioID, TipoNotificacion.Email, cabeceraNotificacion.JsonSerialize(), mensaje, false, false);

                var messageSent = await _sendNotificacionUserCase.Execute(new NotificacionOutput(notificacion.ID, notificacion.FuncionarioID, notificacion.TipoNotificacion.ToString(),
                    notificacion.Cabecera, notificacion.Mensaje, notificacion.Procesado, notificacion.Leido, notificacion.FechaCreacion, notificacion.FechaModificacion));
                if (messageSent)
                {
                    var notificacionProcesada = await _updateNotificationQUserCase.Execute(new NotificacionQueueItem(item.FuncionarioID, item.UserName, ActionType.Alta, QueueStatus.Processed));
                    result = true;
                }
                else
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private string CreateMensajeBody(Declaracion declaracion)
        {
            return declaracion.ID.ToString();
        }
    }
}
