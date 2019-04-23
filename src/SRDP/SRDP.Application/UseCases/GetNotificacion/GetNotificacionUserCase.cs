using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetNotificacion
{
    public class GetNotificacionUserCase : IGetNotificacionUserCase
    {
        private INotificacionReadOnlyRepository _notificacionReadOnlyRepository;

        public GetNotificacionUserCase(INotificacionReadOnlyRepository notificacionReadOnlyRepository)
        {
            _notificacionReadOnlyRepository = notificacionReadOnlyRepository;
        }

        public async Task<NotificacionOutput> Execute(Guid id)
        {
            var notificacion = await _notificacionReadOnlyRepository.Get(id);
            if (notificacion == null) return null;
            return new NotificacionOutput(notificacion.ID, notificacion.FuncionarioID, notificacion.TipoNotificacion.ToString(), 
                notificacion.Cabecera, notificacion.Mensaje, notificacion.Procesado, notificacion.Leido, notificacion.FechaCreacion, notificacion.FechaModificacion);
        }

        public async Task<ICollection<NotificacionOutput>> ExecuteList(int funcionarioID)
        {
            var notificaciones = await _notificacionReadOnlyRepository.GetByFuncionarioID(funcionarioID, false);
            var outputList = new List<NotificacionOutput>();

            if (notificaciones == null) return outputList;

            foreach (var notificacion in notificaciones)
            {
                outputList.Add(new NotificacionOutput(notificacion.ID, notificacion.FuncionarioID, notificacion.TipoNotificacion.ToString(),notificacion.Cabecera, 
                    notificacion.Mensaje, notificacion.Procesado, notificacion.Leido, notificacion.FechaCreacion, notificacion.FechaModificacion));
            }
            return outputList;
        }
    }
}
