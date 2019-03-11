using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.ValueObjects
{
    public class Gestion : ValueObject
    {
        public int Anio { get; }
        public DateTime FechaInicio { get; }
        public DateTime FechaFinal { get; }
        public bool Vigente { get; }
        public bool Habilitada
        {
            get
            {
                return DateTime.Now >= FechaInicio && DateTime.Now <= FechaFinal;
            }
        }

        public Gestion(int anio, DateTime fechaInicio, DateTime fechaFinal, bool vigente)
        {
            if (fechaInicio >= fechaFinal)
                throw new DomainException("La fecha de inicio '"+ fechaInicio.ToLongDateString() +"' no puede ser mayor a la fecha final '"+ fechaFinal.ToLongDateString() + "'" + anio.ToString()) ;
            if (fechaFinal <= fechaInicio)
                throw new DomainException("La fecha de final '"+ fechaFinal.ToLongDateString() + "' no puede ser menor a la fecha final '" + fechaInicio.ToLongDateString() + "'" + anio.ToString());

            Anio = anio;
            FechaInicio = fechaInicio;
            FechaFinal = fechaFinal;
            Vigente = vigente;
        }

        public static Gestion For(int anio, DateTime fechaInicio, DateTime fechaFinal, bool vigente)
        {
            return new Gestion(anio, fechaInicio, fechaFinal, vigente);
        }

        public override string ToString()
        {
            return Anio.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Anio;
            yield return FechaInicio;
            yield return FechaFinal;
            yield return Vigente;
            yield return Habilitada;
        }
    }
}
