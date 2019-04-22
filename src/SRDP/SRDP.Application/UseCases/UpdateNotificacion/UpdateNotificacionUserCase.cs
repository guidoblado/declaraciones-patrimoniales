using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Domain.Enumerations;
using SRDP.Domain.Notificaciones;

namespace SRDP.Application.UseCases.UpdateNotificacion
{
    public class UpdateNotificacionUserCase : IUpdateNotificacionUserCase
    {
        private readonly INotificacionWriteOnlyRepository _notificacionWriteOnlyRepository;
        public UpdateNotificacionUserCase(INotificacionWriteOnlyRepository notificacionWriteOnlyRepository)
        {
            _notificacionWriteOnlyRepository = notificacionWriteOnlyRepository;
        }

        public async Task<NotificacionOutput> Excute(Notificacion notificacion)
        {
            await _notificacionWriteOnlyRepository.Update(notificacion);
            return new NotificacionOutput(notificacion.ID, notificacion.FuncionarioID, notificacion.TipoNotificacion.ToString(),
                notificacion.Cabecera, notificacion.Mensaje, notificacion.Procesado, notificacion.Leido, notificacion.FechaCreacion, notificacion.FechaModificacion);
        }

        public async Task<NotificacionOutput> Excute(int funcionarioID, string tipoNotificacion, string cabecera, string mensaje, bool procesado, bool leido)
        {
            var tipoNotificacionEnum = (TipoNotificacion)Enum.Parse(typeof(TipoNotificacion), tipoNotificacion);
            var notificacion = new Notificacion(funcionarioID, tipoNotificacionEnum, cabecera, mensaje, procesado, leido);
            await _notificacionWriteOnlyRepository.Update(notificacion);
            return new NotificacionOutput(notificacion.ID, notificacion.FuncionarioID, notificacion.TipoNotificacion.ToString(),
                notificacion.Cabecera, notificacion.Mensaje, notificacion.Procesado, notificacion.Leido, notificacion.FechaCreacion, notificacion.FechaModificacion);
        }
    }
}
