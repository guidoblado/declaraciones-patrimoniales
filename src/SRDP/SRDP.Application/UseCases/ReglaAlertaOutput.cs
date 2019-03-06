using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class ReglaAlertaOutput
    {
        public Guid ID { get; private set; }
        public int Gestion { get; private set; }
        public string Descripcion { get; private set; }
        public decimal Porcentaje { get; private set; }
        public string Operador { get; private set; }
        public decimal Monto { get; private set; }

        public ReglaAlertaOutput(Guid id, int gestion, string descripcion, decimal porcentaje, string operador, decimal monto)
        {
            ID = id;
            Gestion = gestion;
            Descripcion = descripcion;
            Porcentaje = porcentaje;
            Operador = operador;
            Monto = monto;
        }
    }
}
