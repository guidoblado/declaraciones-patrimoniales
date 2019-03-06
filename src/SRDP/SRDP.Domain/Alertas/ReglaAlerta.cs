using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Alertas
{
    public class ReglaAlerta : IEntity
    {
        public Guid ID { get; private set; }
        public int Gestion { get; private set; }
        public string Descripcion { get; private set; }
        public decimal PorcentajeVariacion { get; private set; }
        public CondicionAlerta Condicion { get; private set; }
        public decimal Monto { get; private set; }

        #region Consturctors
        private ReglaAlerta() { }

        public ReglaAlerta(int gestion, string descripcion, decimal porcentajeVariacion, CondicionAlerta condicion, decimal monto)
        {
            ID = Guid.NewGuid();
            Gestion = gestion;
            Descripcion = descripcion;
            PorcentajeVariacion = porcentajeVariacion;
            Condicion = condicion;
            Monto = monto;
        }
        #endregion

        public static ReglaAlerta Load(Guid id, int gestion, string descripcion, decimal porcentajeVariacion, CondicionAlerta condicion, decimal monto)
        {
            return new ReglaAlerta
            {
                ID = id,
                Gestion = gestion,
                Descripcion = descripcion,
                PorcentajeVariacion = porcentajeVariacion,
                Condicion = condicion,
                Monto = monto
            };
        }
    }
}
