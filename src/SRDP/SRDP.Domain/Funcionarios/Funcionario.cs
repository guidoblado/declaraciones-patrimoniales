using SRDP.Domain.Enumerations;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Funcionarios
{
    public sealed class Funcionario : IEntity, IAggregateRoot
    {
        #region Public Properties
        public Guid ID { get; private set; }
        public int Codigo { get; private set; }
        public NombreCompleto NombreFuncionario { get; private set; }
        public Cedula CI { get; private set; }
        public DateTime FechaNacimiento { get; private  set; }
        public EstadoCivil EstadoCivil { get; private set; }
        #endregion

        #region Constructors
        public Funcionario(int codigo, NombreCompleto nombreFuncionario, Cedula ci, DateTime fechaNacimiento, EstadoCivil estadocivil)
        {
            ID = Guid.NewGuid();
            Codigo = codigo;
            NombreFuncionario = nombreFuncionario;
            CI = ci;
            FechaNacimiento = fechaNacimiento;
            EstadoCivil = estadocivil;
        }

        private Funcionario() { }
        #endregion

        public static Funcionario Load(Guid id, int codigo, NombreCompleto nombreFuncionario, Cedula ci, DateTime fechaNacimiento, EstadoCivil estadocivil)
        {
            var funcionario = new Funcionario
            {
                ID = id,
                Codigo = codigo,
                NombreFuncionario = nombreFuncionario,
                CI = ci,
                FechaNacimiento = fechaNacimiento,
                EstadoCivil = estadocivil
            };

            return funcionario;
        }
    }
}
