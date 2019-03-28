using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class NotificacionOutput
    {
        public Guid ID { get; private set; }
        public int FuncionarioID { get; private set; }
        public string TipoNotificacion { get; private set; }
        public CabeceraEmailOutput Cabecera { get; private set; }
        public string Mensaje { get; private set; }
        public bool Procesado { get; private set; }
        public bool Leido { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime FechaModificacion { get; private set; }

        public NotificacionOutput(Guid id, int funcionarioID, string tipoNotificacion, string cabecera, string mensaje,
            bool procesado, bool leido, DateTime fechaCreacion, DateTime fechaModificacion)
        {
            ID = id;
            FuncionarioID = funcionarioID;
            TipoNotificacion = tipoNotificacion;
            Cabecera = CabeceraEmailOutput.JsonDeserialize(cabecera);
            Mensaje = mensaje;
            Procesado = procesado;
            Leido = leido;
            FechaCreacion = fechaCreacion;
            FechaModificacion = fechaModificacion;
        }
    }
}
