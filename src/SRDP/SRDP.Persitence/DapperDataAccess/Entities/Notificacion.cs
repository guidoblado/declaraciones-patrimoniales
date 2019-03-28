using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    public class Notificacion
    {
        public Guid ID { get; set; }
        public int FuncionarioID { get; set; }
        public string Tipo { get; set; }
        public string Cabecera { get; set; }
        public string Mensaje { get; set; }
        public bool Procesado { get; set; }
        public bool Leido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
