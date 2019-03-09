using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Inmuebles
{
    public sealed class Inmueble : IEntity
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string Direccion { get; private set; }
        public string TipoDeInmueble { get; private set; }
        public Porcentaje PorcentajeParticipacion { get; private set; }
        public decimal ValorComercial { get; private set; }
        public decimal SaldoHipoteca { get; private set; }
        public string Banco { get; private set; }

        #region Constructors
        public Inmueble(Guid declaracionID, string direccion, string tipoDeInmueble, Porcentaje porcentajeParticipacion, decimal valorComercial, decimal saldoHipoteca, string banco)
        {
            ID = Guid.NewGuid();
            DeclaracionID = declaracionID;
            Direccion = direccion;
            TipoDeInmueble = tipoDeInmueble;
            PorcentajeParticipacion = porcentajeParticipacion;
            ValorComercial = valorComercial;
            SaldoHipoteca = saldoHipoteca;
            Banco = banco;
        }

        private Inmueble() { }
        #endregion

        public static Inmueble Load(Guid id, Guid declaracionID, string direccion, string tipoDeInmueble, Porcentaje porcentajeParticipacion, decimal valorComercial, decimal saldoHipoteca, string banco)
        {
            var inmueble = new Inmueble
            {
                ID = id,
                DeclaracionID = declaracionID,
                Direccion = direccion,
                TipoDeInmueble = tipoDeInmueble,
                PorcentajeParticipacion = porcentajeParticipacion,
                ValorComercial = valorComercial,
                SaldoHipoteca = saldoHipoteca,
                Banco = banco,
            };
            return inmueble;
        }
    }
}
