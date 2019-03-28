using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Notificaciones
{
    public class Notificacion
    {
        public Guid ID { get; private set; }
        public int FuncionarioID { get; private set; }
        public TipoNotificacion TipoNotificacion { get; private set; }
        public string Cabecera { get; private set; }
        public string Mensaje { get; private set; }
        public bool Procesado { get; private set; }
        public bool Leido { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime FechaModificacion { get; private set; }

        #region Constructors
        public Notificacion(int funcionarioID, TipoNotificacion tipoNotificacion, string cabecera, string mensaje,
            bool procesado, bool leido)
        {
            ID = Guid.NewGuid();
            FuncionarioID = funcionarioID;
            TipoNotificacion = tipoNotificacion;
            Cabecera = cabecera;
            Mensaje = mensaje;
            Procesado = procesado;
            Leido = leido;
            FechaCreacion = DateTime.Now;
            FechaModificacion = DateTime.Now;
        }

        private Notificacion() { }

        #endregion

        public static Notificacion Load(Guid id, int funcionarioID, TipoNotificacion tipoNotificacion, string cabecera, string mensaje,
            bool procesado, bool leido, DateTime fechaCreacion, DateTime fechaModificacion)
        {
            return new Notificacion
            {
                ID = id,
                FuncionarioID = funcionarioID,
                TipoNotificacion = tipoNotificacion,
                Cabecera = cabecera,
                Mensaje = mensaje,
                Procesado = procesado,
                Leido = leido,
                FechaCreacion = fechaCreacion,
                FechaModificacion = fechaModificacion,
            };
        }
    }
}
