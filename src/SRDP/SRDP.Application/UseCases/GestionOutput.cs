using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class GestionOutput
    {
        public int Gestion { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFinal { get; private set; }
        public bool Vigente { get; private set; }

        public GestionOutput(int gestion, DateTime fechaInicio, DateTime fechaFinal, bool vigente)
        {
            Gestion = gestion;
            FechaInicio = fechaInicio;
            FechaFinal = FechaFinal;
            Vigente = vigente;
        }
    }
}
