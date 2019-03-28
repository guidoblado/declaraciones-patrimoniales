using SRDP.Domain.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.UpdateNotificacion
{
    public interface IUpdateNotificacionUserCase
    {
        Task<NotificacionOutput> Excute(Notificacion notificacion);
        Task<NotificacionOutput> Excute(int funcionarioID, string tipoNotificacion, string cabecera, string mensaje, bool procesado, bool leido);
    }
}
