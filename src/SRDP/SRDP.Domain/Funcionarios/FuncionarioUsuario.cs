using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Funcionarios
{
    public class FuncionarioUsuario
    {
        public int FuncionarioID { get; private set; }
        public string NombreUsuario { get; private set; }
        public NombreCompleto NombreCompleto { get; private set; }
        public int EstadoID { get; private set; }
        public string Estado { get; private set; }
        public string Email { get; private set; }

        #region Constructors
        private FuncionarioUsuario() { }

        public FuncionarioUsuario(int funcionarioID, string nombreUsuario, NombreCompleto nombreCompleto, int estadoID, string estado, string email)
        {
            FuncionarioID = funcionarioID;
            NombreUsuario = nombreUsuario;
            NombreCompleto = nombreCompleto;
            EstadoID = estadoID;
            Estado = estado;
            Email = email;
        }
        #endregion

        public static FuncionarioUsuario Load(int funcionarioID, string nombreUsuario, NombreCompleto nombreCompleto, int estadoID, string estado, string email)
        {
            return new FuncionarioUsuario
            {
                FuncionarioID = funcionarioID,
                NombreUsuario = nombreUsuario,
                NombreCompleto = nombreCompleto,
                EstadoID = estadoID,
                Estado = estado,
                Email = email,
            };
        }
    }
}
