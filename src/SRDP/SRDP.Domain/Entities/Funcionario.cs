using SRDP.Domain.Enumerations;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Entities
{
    public class Funcionario
    {
        public int FuncionarioID { get; set; }
        public NombreCompleto NombreFuncionario { get; set; }
        public Cedula Cedula { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public EstadoCivil  EstadoCivil { get; set; }
        

        public Funcionario()
        {
        }
    }
}
