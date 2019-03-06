using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.ValueObjects
{
    public class NombreCompleto : ValueObject
    {
        public string Nombre { get; }
        public string ApellidoPaterno { get; }
        public string ApellidoMaterno { get; }
       
        private NombreCompleto()
        { }

        public NombreCompleto(string nombre, string apellidoPaterno, string apellidoMaterno)
        {
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
        }

        public NombreCompleto(string nombre, string apellidos)
        {
            Nombre = nombre;
            ApellidoPaterno = apellidos;
            ApellidoMaterno = String.Empty;
        }
        public override string ToString()
        {
            return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Nombre;
            yield return ApellidoPaterno;
            yield return ApellidoMaterno;
        }
    }
}
