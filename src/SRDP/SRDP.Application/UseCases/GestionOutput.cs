using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class GestionOutput
    {
        public int Anio { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFinal { get; private set; }
        public bool Vigente { get; private set; }

        public GestionOutput(int anio, DateTime fechaInicio, DateTime fechaFinal, bool vigente)
        {
            Anio = anio;
            FechaInicio = fechaInicio;
            FechaFinal = fechaFinal;
            Vigente = vigente;
        }
    }
}
